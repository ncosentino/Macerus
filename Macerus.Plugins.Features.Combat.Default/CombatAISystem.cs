using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class CombatAISystem : IDiscoverableSystem
    {
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly ITurnBasedManager _turnBasedManager;
        private readonly ILogger _logger;
        private readonly ICombatGameObjectProvider _combatGameObjectProvider;
        private readonly ICombatAIFactory _combatAIFactory;
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;
        private IGameObject _currentActor;
        private ICombatAI _currentCombatAI;

        public int? Priority => null;

        public CombatAISystem(
            IObservableCombatTurnManager combatTurnManager,
            ITurnBasedManager turnBasedManager,
            ILogger logger, 
            ICombatGameObjectProvider combatGameObjectProvider,
            ICombatAIFactory combatAIFactory,
            ICombatStatIdentifiers combatStatIdentifiers)
        {
            _combatTurnManager = combatTurnManager;
            _turnBasedManager = turnBasedManager;
            _logger = logger;
            _combatGameObjectProvider = combatGameObjectProvider;
            _combatAIFactory = combatAIFactory;
            _combatStatIdentifiers = combatStatIdentifiers;

            _combatTurnManager.TurnProgressed += CombatTurnManager_TurnProgressed;
            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
        }

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            var currentActor = _currentActor;
            var currentCombatAI = _currentCombatAI;

            if (!_combatTurnManager.InCombat ||
                currentActor == null ||
                currentCombatAI == null)
            {
                return;
            }

            var elapsed = (IInterval<double>)systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;

            var currentLife = currentActor
                .GetOnly<IHasStatsBehavior>()
                // FIXME: do we need to consider a stat calc or
                // can we assume base stat
                .BaseStats[_combatStatIdentifiers.CurrentLifeStatId];
            if (currentLife <= 0)
            {
                _turnBasedManager.SetApplicableObjects(new[] { currentActor });
                return;
            }

            if (currentCombatAI.Execute(
                currentActor,
                new HashSet<IGameObject>(_combatGameObjectProvider.GetGameObjects()),
                elapsed))
            {
                _turnBasedManager.SetApplicableObjects(new[] { currentActor });
            }
        }

        private ICombatAI GetCombatAI(IGameObject currentActor)
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

                return null;
            }

            var combatAI = _combatAIFactory.Create();
            return combatAI;
        }

        private void ConfigureForNextActor(IGameObject nextActor)
        {
            _currentActor = nextActor;
            _currentCombatAI = GetCombatAI(nextActor);
        }

        private void CombatTurnManager_TurnProgressed(
            object sender,
            TurnProgressedEventArgs e)
        {
            var nextActor = e.ActorWithNextTurn;
            ConfigureForNextActor(nextActor);
        }

        private void CombatTurnManager_CombatStarted(
            object sender,
            CombatStartedEventArgs e)
        {
            var nextActor = e.ActorOrder.First();
            ConfigureForNextActor(nextActor);
        }
    }
}
