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
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly ILogger _logger;

        public EncounterCombatStartHandler(
            ICombatTurnManager combatTurnManager,
            ITurnBasedManager turnBasedManager,
            IFilterContextProvider filterContextProvider,
            ILogger logger)
        {
            _combatTurnManager = combatTurnManager;
            _turnBasedManager = turnBasedManager;
            _filterContextProvider = filterContextProvider;
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

            _combatTurnManager.Reset();

            _logger.Debug("Initial combat order:");
            foreach (var actor in _combatTurnManager.GetSnapshot(
                _filterContextProvider.GetContext(),
                10))
            {
                _logger.Debug($"\t{actor}");
            }
        }
    }
}
