using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default
{
    public sealed class ActorDeathAnimationSystem : IDiscoverableSystem
    {
        private readonly IBehaviorFinder _behaviorFinder;
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;

        private double _nextTriggerAccumulator;

        public ActorDeathAnimationSystem(
            IBehaviorFinder behaviorFinder,
            IActorIdentifiers actorIdentifiers,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ICombatStatIdentifiers combatStatIdentifiers)
        {
            _behaviorFinder = behaviorFinder;
            _actorIdentifiers = actorIdentifiers;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _combatStatIdentifiers = combatStatIdentifiers;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;
            if (turnInfo.ElapsedTurns == 0)
            {
                return;
            }
            else if (turnInfo.ElapsedTurns != 1)
            {
                _nextTriggerAccumulator += turnInfo.ElapsedTurns;
                const double TURNS_BEFORE_UPDATING = 0.3;
                if (_nextTriggerAccumulator < TURNS_BEFORE_UPDATING)
                {
                    return;
                }
            }

            _nextTriggerAccumulator = 0;

            // we want to iterate over all the game objects, not just the ones
            // with the current turn, especially because the actor dying is
            // usually not the one with the active turn
            foreach (var entry in GetSupportedEntries(turnInfo.AllGameObjects))
            {
                var currentLife = _statCalculationServiceAmenity.GetStatValue(
                    entry.Item1.Owner,
                    _combatStatIdentifiers.CurrentLifeStatId);
                if (currentLife <= 0)
                {
                    entry.Item2.BaseAnimationId = _actorIdentifiers.AnimationDeath;
                }
            }
        }

        private IEnumerable<Tuple<IHasStatsBehavior, IDynamicAnimationBehavior>> GetSupportedEntries(IEnumerable<IGameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                Tuple<IHasStatsBehavior, IDynamicAnimationBehavior> requiredBehaviors;
                if (!_behaviorFinder.TryFind(
                    gameObject,
                    out requiredBehaviors))
                {
                    continue;
                }

                yield return requiredBehaviors;
            }
        }
    }
}
