using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Encounters.EndHandlers;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.Encounters.Default.StartHandlers
{
    public sealed class EncounterCombatStartHandler : 
        IDiscoverableStartEncounterHandler, 
        IDiscoverableEndEncounterHandler
    {
        private readonly Lazy<ICombatTurnManager> _lazyCombatTurnManager;
        private readonly Lazy<IEncounterManager> _lazyEncounterManager;
        private readonly Lazy<IFilterContextAmenity> _lazyFilterContextAmenity;
        private readonly Lazy<ICombatTeamIdentifiers> _lazyCombatTeamIdentifiers;
        private readonly IEncounterIdentifiers _encounterIdentifiers;

        public EncounterCombatStartHandler(
            Lazy<ICombatTurnManager> lazyCombatTurnManager,
            Lazy<IEncounterManager> lazyEncounterManager,
            Lazy<IFilterContextAmenity> lazyFilterContextAmenity,
            Lazy<ICombatTeamIdentifiers> lazyCombatTeamIdentifiers,
            IEncounterIdentifiers encounterIdentifiers)
        {
            _lazyCombatTurnManager = lazyCombatTurnManager;
            _lazyEncounterManager = lazyEncounterManager;
            _lazyFilterContextAmenity = lazyFilterContextAmenity;
            _lazyCombatTeamIdentifiers = lazyCombatTeamIdentifiers;
            _encounterIdentifiers = encounterIdentifiers;
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

        async Task<IGameObject> IEndEncounterHandler.HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            // safety mechanism to unhook in case combat never ended before we need to handle again
            _lazyCombatTurnManager.Value.CombatEnded -= CombatTurnManager_CombatEnded;
            return encounter;
        }

        private async void CombatTurnManager_CombatEnded(object sender, CombatEndedEventArgs e)
        {
            _lazyCombatTurnManager.Value.CombatEnded -= CombatTurnManager_CombatEnded;
            var playerWon = e
                .WinningTeam
                ?.Any(x => x.GetOnly<IHasStatsBehavior>().BaseStats[_lazyCombatTeamIdentifiers.Value.CombatTeamStatDefinitionId] == _lazyCombatTeamIdentifiers.Value.PlayerTeamStatValue);

            var filterContext = _lazyFilterContextAmenity.Value.GetContext();
            filterContext = _lazyFilterContextAmenity
                .Value
                .ExtendWithSupported(filterContext, new IFilterAttribute[]
                {
                    _lazyFilterContextAmenity
                        .Value
                        .CreateSupportedAttribute(
                            _encounterIdentifiers.FilterEncounterCombatPlayerWonId,
                            playerWon == true),
                });

            await _lazyEncounterManager
                .Value
                .EndEncounterAsync(
                    filterContext,
                    new IBehavior[]
                    {
                        new EncounterCombatOutcomeBehavior(
                            e.WinningTeam,
                            e.LosingTeams,
                            playerWon),
                    })
                .ConfigureAwait(false);
        }
    }
}
