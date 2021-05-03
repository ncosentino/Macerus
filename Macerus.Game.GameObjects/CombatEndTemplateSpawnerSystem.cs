using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Api.GameObjects;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Game
{
    public sealed class CombatEndTemplateSpawnerSystem : IDiscoverableSystem
    {
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IBehaviorFinder _behaviorFinder;
        private readonly IGameObjectRepositoryAmenity _gameObjectRepositoryAmenity;
        private readonly ILogger _logger;

        public CombatEndTemplateSpawnerSystem(
            IMapGameObjectManager mapGameObjectManager,
            IBehaviorFinder behaviorFinder,
            IObservableCombatTurnManager combatTurnManager,
            IGameObjectRepositoryAmenity gameObjectRepositoryAmenity)
        {
            _mapGameObjectManager = mapGameObjectManager;
            _behaviorFinder = behaviorFinder;
            _gameObjectRepositoryAmenity = gameObjectRepositoryAmenity;
            combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
        }

        public int? Priority => null;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IGameObject> gameObjects)
        {
            // no-op
        }

        private void CombatTurnManager_CombatEnded(
            object sender,
            CombatEndedEventArgs e)
        {
            var spawnersToTrigger = _mapGameObjectManager
                .GameObjects
                .Select(x =>
                {
                    if (!_behaviorFinder.TryFind<ITriggerOnCombatEndBehavior, IReadOnlySpawnTemplatePropertiesBehavior>(
                        x,
                        out var behaviors))
                    {
                        return null;
                    }

                    return new
                    {
                        GameObject = x,
                        TriggerOnCombatEnd = behaviors.Item1,
                        SpawnTemplateProperties = behaviors.Item2,
                    };
                })
                .Where(x => x != null);
            foreach (var entry in spawnersToTrigger)
            {
                var spawnedObject = _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                    entry.SpawnTemplateProperties.TypeId,
                    entry.SpawnTemplateProperties.TemplateId,
                    entry.SpawnTemplateProperties.Properties);
                _mapGameObjectManager.MarkForRemoval(entry.GameObject);
                _mapGameObjectManager.MarkForAddition(spawnedObject);
            }
        }
    }
}
