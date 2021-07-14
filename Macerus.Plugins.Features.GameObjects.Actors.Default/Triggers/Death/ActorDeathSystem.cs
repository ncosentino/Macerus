using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Triggers;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Triggers.Death
{
    public sealed class ActorDeathSystem : IDiscoverableSystem
    {
        private readonly IBehaviorFinder _behaviorFinder;
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;
        private readonly IDeathTriggerMechanicSource _deathTriggerMechanicSource;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly ConcurrentDictionary<IGameObject, IHasStatsBehavior> _actors;

        private double _nextTriggerAccumulator;

        public ActorDeathSystem(
            IBehaviorFinder behaviorFinder,
            IActorIdentifiers actorIdentifiers,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ICombatStatIdentifiers combatStatIdentifiers,
            IDeathTriggerMechanicSource deathTriggerMechanicSource,
            IReadOnlyMapGameObjectManager mapGameObjectManager)
        {
            _actors = new ConcurrentDictionary<IGameObject, IHasStatsBehavior>();
            _behaviorFinder = behaviorFinder;
            _actorIdentifiers = actorIdentifiers;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _combatStatIdentifiers = combatStatIdentifiers;
            _deathTriggerMechanicSource = deathTriggerMechanicSource;
            _mapGameObjectManager = mapGameObjectManager;
            _mapGameObjectManager.Synchronized += MapGameObjectManager_Synchronized;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            var elapsedTime = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value;

            _nextTriggerAccumulator += ((IInterval<double>)elapsedTime.Interval).Value / 1000;
            const double TURNS_BEFORE_UPDATING = 0.3;
            if (_nextTriggerAccumulator < TURNS_BEFORE_UPDATING)
            {
                return;
            }

            _nextTriggerAccumulator = 0;

            foreach (var entry in _actors)
            {
                await Task.Yield();
                var currentLife = _statCalculationServiceAmenity.GetStatValue(
                    entry.Key,
                    _combatStatIdentifiers.CurrentLifeStatId);
                if (currentLife <= 0)
                {
                    await _deathTriggerMechanicSource.ActorDeathTriggeredAsync(entry.Key);
                }
            }
        }

        private IEnumerable<Tuple<IHasStatsBehavior, ITypeIdentifierBehavior>> GetSupportedEntries(IEnumerable<IGameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                Tuple<IHasStatsBehavior, ITypeIdentifierBehavior> requiredBehaviors;
                if (!_behaviorFinder.TryFind(
                    gameObject,
                    out requiredBehaviors) ||
                    !Equals(requiredBehaviors.Item2.TypeId, _actorIdentifiers.ActorTypeIdentifier))
                {
                    continue;
                }

                yield return requiredBehaviors;
            }
        }

        private void MapGameObjectManager_Synchronized(
            object sender,
            GameObjectsSynchronizedEventArgs e)
        {
            foreach (var supportedEntry in GetSupportedEntries(e.Added))
            {
                _actors[supportedEntry.Item1.Owner] = supportedEntry.Item1;
            }

            foreach (var removed in e.Removed)
            {
                _actors.TryRemove(removed, out _);
            }
        }
    }
}
