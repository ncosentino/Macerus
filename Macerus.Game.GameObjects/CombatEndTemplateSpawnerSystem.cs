using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Api.GameObjects;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Game
{
    public sealed class CombatEndTemplateSpawnerSystem : IDiscoverableSystem
    {
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IBehaviorFinder _behaviorFinder;
        private readonly IGameObjectRepositoryAmenity _gameObjectRepositoryAmenity;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly ILogger _logger;

        public CombatEndTemplateSpawnerSystem(
            IMapGameObjectManager mapGameObjectManager,
            IBehaviorFinder behaviorFinder,
            IObservableCombatTurnManager combatTurnManager,
            IGameObjectRepositoryAmenity gameObjectRepositoryAmenity,
            IGameObjectFactory gameObjectFactory)
        {
            _mapGameObjectManager = mapGameObjectManager;
            _behaviorFinder = behaviorFinder;
            _gameObjectRepositoryAmenity = gameObjectRepositoryAmenity;
            _gameObjectFactory = gameObjectFactory;
            combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            // no-op... hijacking as a system to be resolved easily
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
                var templateToSpawn = entry.SpawnTemplateProperties.TemplateToSpawn;
                
                var spawnedObject = templateToSpawn.TryGetFirst<ITemplateIdentifierBehavior>(out var templateIdentifierBehavior)
                    ? _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                        templateIdentifierBehavior.TemplateId,
                        templateToSpawn.Behaviors)
                    : _gameObjectFactory.Create(templateToSpawn.Behaviors);
                _mapGameObjectManager.MarkForRemoval(entry.GameObject);
                _mapGameObjectManager.MarkForAddition(spawnedObject);
            }
        }
    }
}
