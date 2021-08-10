using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Triggers;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Triggers.Death
{
    public sealed class ActorDeathSystem : IDiscoverableSystem
    {
        private readonly IBehaviorFinder _behaviorFinder;
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;
        private readonly IDeathTriggerMechanicSource _deathTriggerMechanicSource;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;

        public ActorDeathSystem(
            IBehaviorFinder behaviorFinder,
            IActorIdentifiers actorIdentifiers,
            ICombatStatIdentifiers combatStatIdentifiers,
            IDeathTriggerMechanicSource deathTriggerMechanicSource,
            IReadOnlyMapGameObjectManager mapGameObjectManager)
        {
            _behaviorFinder = behaviorFinder;
            _actorIdentifiers = actorIdentifiers;
            _combatStatIdentifiers = combatStatIdentifiers;
            _deathTriggerMechanicSource = deathTriggerMechanicSource;
            _mapGameObjectManager = mapGameObjectManager;
            _mapGameObjectManager.Synchronized += MapGameObjectManager_Synchronized;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
        }

        private IEnumerable<IHasStatsBehavior> FilterGameObjectsForActorStats(IEnumerable<IGameObject> gameObjects)
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

                yield return requiredBehaviors.Item1;
            }
        }

        private void MapGameObjectManager_Synchronized(
            object sender,
            GameObjectsSynchronizedEventArgs e)
        {
            foreach (var hasStatsBehavior in FilterGameObjectsForActorStats(e.Added))
            {
                hasStatsBehavior.BaseStatsChanged += HasStatsBehavior_BaseStatsChanged;
            }

            foreach (var hasStatsBehavior in FilterGameObjectsForActorStats(e.Removed))
            {
                hasStatsBehavior.BaseStatsChanged -= HasStatsBehavior_BaseStatsChanged;
            }
        }

        private async void HasStatsBehavior_BaseStatsChanged(
            object sender,
            StatsChangedEventArgs e)
        {
            if (!e.ChangedStats.Any(x => Equals(x.Key, _combatStatIdentifiers.CurrentLifeStatId)))
            {
                return;
            }

            var hasStatsBehavior = (IHasStatsBehavior)sender;
            var currentLife = hasStatsBehavior.BaseStats[_combatStatIdentifiers.CurrentLifeStatId];
            if (currentLife > 0)
            {
                return;
            }

            await _deathTriggerMechanicSource
                .ActorDeathTriggeredAsync(hasStatsBehavior.Owner)
                .ConfigureAwait(false);
        }
    }
}
