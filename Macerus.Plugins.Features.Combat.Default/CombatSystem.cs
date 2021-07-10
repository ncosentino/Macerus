using System.Threading.Tasks;

using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class CombatSystem : IDiscoverableSystem
    {
        private readonly ITurnBasedManager _turnBasedManager;
        private readonly ICombatTurnManager _combatTurnManager;
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly IWinConditionHandlerFacade _winConditionHandler;
        private readonly ILogger _logger;

        private double _nextWinConditionCheckAccumulator;

        public CombatSystem(
            ICombatTurnManager combatTurnManager,
            IFilterContextProvider filterContextProvider,
            IWinConditionHandlerFacade winConditionHandler,
            ILogger logger,
            ITurnBasedManager turnBasedManager)
        {
            _combatTurnManager = combatTurnManager;
            _filterContextProvider = filterContextProvider;
            _winConditionHandler = winConditionHandler;
            _logger = logger;
            _turnBasedManager = turnBasedManager;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            if (!_combatTurnManager.InCombat)
            {
                return;
            }

            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;
            var elapsedTime = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value;

            if (turnInfo.ElapsedTurns == 1)
            {
                var filterContext = _filterContextProvider.GetContext();
                _combatTurnManager.ProgressTurn(
                    filterContext,
                    1);
            }

            const double SECONDS_BEFORE_WIN_CHECK = 0.3;
            _nextWinConditionCheckAccumulator += ((IInterval<double>)elapsedTime.Interval).Value / 1000;
            if (_nextWinConditionCheckAccumulator >= SECONDS_BEFORE_WIN_CHECK)
            {
                _nextWinConditionCheckAccumulator = 0;

                if (_winConditionHandler.CheckWinConditions(
                    out var winningTeam,
                    out var losingTeams))
                {
                    _turnBasedManager.GlobalSync = true;
                    _turnBasedManager.SyncTurnsFromElapsedTime = true;
                    _combatTurnManager.EndCombat(
                        winningTeam,
                        losingTeams);
                }
            }
        }
    }
}
