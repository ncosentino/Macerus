﻿using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class CombatAISystem : IDiscoverableSystem
    {
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly ITurnBasedManager _turnBasedManager;
        private readonly ILogger _logger;
        private readonly ICombatGameObjectProvider _combatGameObjectProvider;
        private readonly ICombatAIFactory _combatAIFactory;

        private IGameObject _currentActor;
        private ICombatAI _currentCombatAI;

        public int? Priority => null;

        public CombatAISystem(
            IObservableCombatTurnManager combatTurnManager,
            ITurnBasedManager turnBasedManager,
            ILogger logger, 
            ICombatGameObjectProvider combatGameObjectProvider,
            ICombatAIFactory combatAIFactory)
        {
            _combatTurnManager = combatTurnManager;
            _turnBasedManager = turnBasedManager;
            _logger = logger;
            _combatTurnManager.TurnProgressed += CombatTurnManager_TurnProgressed;
            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            _combatGameObjectProvider = combatGameObjectProvider;
            _combatAIFactory = combatAIFactory;
        }

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            if (!_combatTurnManager.InCombat ||
                _currentActor == null ||
                _currentCombatAI == null)
            {
                return;
            }

            var elapsed = (IInterval<double>)systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;

            if (_currentCombatAI.Execute(
                _currentActor,
                new HashSet<IGameObject>(_combatGameObjectProvider.GetGameObjects()),
                elapsed))
            {
                _turnBasedManager.SetApplicableObjects(new[] { _currentActor });
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

        private void CombatTurnManager_TurnProgressed(
            object sender,
            TurnProgressedEventArgs e)
        {
            _currentActor = e.ActorWithNextTurn;
            _currentCombatAI = GetCombatAI(_currentActor);
        }

        private void CombatTurnManager_CombatStarted(
            object sender,
            CombatStartedEventArgs e)
        {
            _currentActor = e.ActorOrder.First();
            _currentCombatAI = GetCombatAI(_currentActor);
        }
    }
}