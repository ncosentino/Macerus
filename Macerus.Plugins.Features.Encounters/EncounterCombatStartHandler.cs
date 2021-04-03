using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterCombatStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly ITurnBasedManager _turnBasedManager;
        private readonly ICombatTurnManager _combatTurnManager;
        private readonly ILogger _logger;

        public EncounterCombatStartHandler(
            ICombatTurnManager combatTurnManager,
            ITurnBasedManager turnBasedManager,
            ILogger logger)
        {
            _combatTurnManager = combatTurnManager;
            _turnBasedManager = turnBasedManager;
            _logger = logger;
        }

        public int Priority => int.MaxValue;

        public void Handle(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            _turnBasedManager.ClearApplicableOnUpdate = true;
            _turnBasedManager.GlobalSync = false;
            _turnBasedManager.SyncTurnsFromElapsedTime = false;

            _combatTurnManager.StartCombat(filterContext);

            _logger.Debug("Initial combat order:");
            foreach (var actor in _combatTurnManager.GetSnapshot(
                filterContext,
                10))
            {
                _logger.Debug($"\t{actor}");
            }
        }
    }
}
