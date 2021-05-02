using System;
using System.Collections.Generic;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorDeathAnimationSystem : IDiscoverableSystem
    {
        private readonly IBehaviorFinder _behaviorFinder;
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;

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

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            // FIXME: update interval throttling?

            foreach (var entry in GetSupportedEntries(hasBehaviors))
            {
                var currentLife = _statCalculationServiceAmenity.GetStatValue(
                    (IGameObject)entry.Item1.Owner, // FIXME: casting. reduce usage of IHasBehaviors
                    _combatStatIdentifiers.CurrentLifeStatId);
                if (currentLife <= 0)
                {
                    entry.Item2.CurrentAnimationId = _actorIdentifiers.AnimationDeath;
                }
            }
        }

        private IEnumerable<Tuple<IHasStatsBehavior, IDynamicAnimationBehavior>> GetSupportedEntries(IEnumerable<IHasBehaviors> hasBehaviors)
        {
            foreach (var gameObject in hasBehaviors)
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
