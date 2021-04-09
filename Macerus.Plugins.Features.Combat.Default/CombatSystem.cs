using System.Collections.Generic;

using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class CombatSystem : IDiscoverableSystem
    {
        private readonly ICombatTurnManager _combatTurnManager;
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly IWinConditionHandlerFacade _winConditionHandler;
        private readonly ILogger _logger;

        public CombatSystem(
            ICombatTurnManager combatTurnManager,
            IFilterContextProvider filterContextProvider,
            IWinConditionHandlerFacade winConditionHandler,
            ILogger logger)
        {
            _combatTurnManager = combatTurnManager;
            _filterContextProvider = filterContextProvider;
            _winConditionHandler = winConditionHandler;
            _logger = logger;
        }

        public int? Priority => null;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;
            if (!InCombat(turnInfo))
            {
                return;
            }

            var filterContext = _filterContextProvider.GetContext();
            _combatTurnManager.ProgressTurn(
                filterContext,
                1);

            if (_winConditionHandler.TryGetWinningTeam(out var winningTeamId))
            {
                _logger.Debug(
                    $"FIXME: handle winning team {winningTeamId}");
            }
        }        

        private bool InCombat(ITurnInfo turnInfo)
        {
            // FIXME: we need a better way to check if we're in combat
            return
                turnInfo.ElapsedTurns == 1 &&
                !turnInfo.GlobalSync;
        }
    }
}
