using System;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters.Default.StartHandlers
{
    public sealed class EncounterCombatStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly Lazy<ICombatTurnManager> _lazyCombatTurnManager;
        private readonly Lazy<IEncounterManager> _lazyEncounterManager;
        private readonly Lazy<IFilterContextProvider> _lazyFilterContextProvider;

        public EncounterCombatStartHandler(
            Lazy<ICombatTurnManager> lazyCombatTurnManager,
            Lazy<IEncounterManager> lazyEncounterManager,
            Lazy<IFilterContextProvider> lazyFilterContextProvider)
        {
            _lazyCombatTurnManager = lazyCombatTurnManager;
            _lazyEncounterManager = lazyEncounterManager;
            _lazyFilterContextProvider = lazyFilterContextProvider;

            // FIXME: sorta defeats the point of lazy to hook here
            _lazyEncounterManager.Value.EncounterChanged += EncounterManager_EncounterChanged;
        }

        private void EncounterManager_EncounterChanged(object sender, EncounterChangedEventArgs e)
        {
            _lazyCombatTurnManager.Value.CombatEnded -= CombatTurnManager_CombatEnded;
        }

        public async Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            if (!encounter.TryGetFirst<IEncounterCombatBehavior>(out var combatBehavior))
            {
                return;
            }

            _lazyCombatTurnManager.Value.CombatEnded += CombatTurnManager_CombatEnded;
            await _lazyCombatTurnManager
                .Value
                .StartCombatAsync(filterContext)
                .ConfigureAwait(false);
        }

        private async void CombatTurnManager_CombatEnded(object sender, CombatEndedEventArgs e)
        {
            _lazyCombatTurnManager.Value.CombatEnded -= CombatTurnManager_CombatEnded;

            var filterContext = _lazyFilterContextProvider.Value.GetContext();
            await _lazyEncounterManager
                .Value
                .EndEncounterAsync(filterContext)
                .ConfigureAwait(false);
        }
    }
}
