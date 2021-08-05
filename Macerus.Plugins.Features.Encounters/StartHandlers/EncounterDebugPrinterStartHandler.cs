using System.Threading.Tasks;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterDebugPrinterStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly IReadOnlyCombatTurnManager _combatTurnManager;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ICombatTeamIdentifiers _combatTeamIdentifiers;
        private readonly ILogger _logger;

        public EncounterDebugPrinterStartHandler(
            IReadOnlyCombatTurnManager combatTurnManager,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ICombatTeamIdentifiers combatTeamIdentifiers,
            ILogger logger)
        {
            _combatTurnManager = combatTurnManager;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _combatTeamIdentifiers = combatTeamIdentifiers;
            _logger = logger;
        }

        public int Priority => int.MaxValue;

        public async Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            _logger.Debug("Initial combat order:");
            foreach (var actor in _combatTurnManager.GetSnapshot(
                filterContext,
                10))
            {
                var team = _statCalculationServiceAmenity.GetStatValue(
                    actor,
                    _combatTeamIdentifiers.CombatTeamStatDefinitionId);
                _logger.Debug($"\t{actor}, Team {team}");
            }
        }
    }
}
