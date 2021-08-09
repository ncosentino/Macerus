using System;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Encounters.EndHandlers;
using Macerus.Plugins.Features.Gui;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters.Default.StartHandlers
{
    public sealed class EncounterCombatStartHandler : 
        IDiscoverableStartEncounterHandler, 
        IDiscoverableEndEncounterHandler
    {
        private readonly Lazy<ICombatTurnManager> _lazyCombatTurnManager;
        private readonly Lazy<IEncounterManager> _lazyEncounterManager;
        private readonly Lazy<IFilterContextProvider> _lazyFilterContextProvider;
        private readonly Lazy<IModalManager> _lazyModalManager;

        public EncounterCombatStartHandler(
            Lazy<ICombatTurnManager> lazyCombatTurnManager,
            Lazy<IEncounterManager> lazyEncounterManager,
            Lazy<IFilterContextProvider> lazyFilterContextProvider,
            Lazy<IModalManager> lazyModalManager)
        {
            _lazyCombatTurnManager = lazyCombatTurnManager;
            _lazyEncounterManager = lazyEncounterManager;
            _lazyFilterContextProvider = lazyFilterContextProvider;
            _lazyModalManager = lazyModalManager;
        }

        async Task IStartEncounterHandler.HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            // safety mechanism to unhook in case combat never ended before we need to handle again
            _lazyCombatTurnManager.Value.CombatEnded -= CombatTurnManager_CombatEnded;

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

        async Task IEndEncounterHandler.HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            // safety mechanism to unhook in case combat never ended before we need to handle again
            _lazyCombatTurnManager.Value.CombatEnded -= CombatTurnManager_CombatEnded;
        }

        private async void CombatTurnManager_CombatEnded(object sender, CombatEndedEventArgs e)
        {
            _lazyCombatTurnManager.Value.CombatEnded -= CombatTurnManager_CombatEnded;

            var filterContext = _lazyFilterContextProvider.Value.GetContext();
            await _lazyEncounterManager
                .Value
                .EndEncounterAsync(filterContext)
                .ConfigureAwait(false);

            await _lazyModalManager
                .Value
                .ShowAndWaitMessageBoxAsync("// FIXME: put a fun win screen here!")
                .ConfigureAwait(false);
        }
    }
}
