using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Combat.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterCombatStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly ICombatTurnManager _combatTurnManager;
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly ILogger _logger;

        public EncounterCombatStartHandler(
            ICombatTurnManager combatTurnManager,
            IFilterContextProvider filterContextProvider,
            ILogger logger)
        {
            _combatTurnManager = combatTurnManager;
            _filterContextProvider = filterContextProvider;
            _logger = logger;
        }

        public int Priority => int.MaxValue;

        public void Handle(
            IGameObject encounter,
            IFilterContext filterContext)
        {
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
