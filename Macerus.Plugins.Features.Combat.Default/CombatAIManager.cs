using System;
using System.Linq;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class CombatAIManager
    {
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly ITurnBasedManager _turnBasedManager;
        private readonly ILogger _logger;

        public CombatAIManager(
            IObservableCombatTurnManager combatTurnManager,
            ITurnBasedManager turnBasedManager,
            ILogger logger)
        {
            _combatTurnManager = combatTurnManager;
            _turnBasedManager = turnBasedManager;
            _logger = logger;
            _combatTurnManager.TurnProgressed += CombatTurnManager_TurnProgressed;
            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
        }

        private bool TryHandleAI(IGameObject currentActor)
        {
            if (!currentActor.TryGetFirst<ICombatAIBehavior>(out var combatAIBehavior))
            {
                if (!currentActor.Has<IPlayerControlledBehavior>())
                {
                    throw new InvalidOperationException(
                        $"The current actor '{currentActor}' does not have " +
                        $"either '{nameof(ICombatAIBehavior)}' or " +
                        $"'{nameof(IPlayerControlledBehavior)}'. '{this}' " +
                        $"cannot determine how to support this turn. The actor " +
                        $"may be missing a behavior.");
                }

                return false;
            }

            _logger.Debug(
                $"FIXME: {currentActor}.{combatAIBehavior} exists. No-op for now!");
            _turnBasedManager.SetApplicableObjects(new[] { currentActor });

            return true;
        }

        private void CombatTurnManager_TurnProgressed(
            object sender,
            TurnOrderEventArgs e)
        {
            var currentActor = e.ActorOrder.First();
            TryHandleAI(currentActor);
        }

        private void CombatTurnManager_CombatStarted(
            object sender,
            TurnOrderEventArgs e)
        {
            var currentActor = e.ActorOrder.First();
            TryHandleAI(currentActor);
        }
    }
}
