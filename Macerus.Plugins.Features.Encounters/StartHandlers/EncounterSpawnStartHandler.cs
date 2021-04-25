
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterSpawnStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly ISpawnTableRepositoryFacade _spawnTableRepositoryFacade;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly IEncounterGameObjectPlacer _encounterGameObjectPlacer;
        private readonly ISpawnTableIdentifiers _spawnTableIdentifiers;
        private readonly IActorSpawner _actorSpawner;
        private readonly IFilterContextAmenity _filterContextAmenity;

        public EncounterSpawnStartHandler(
            ISpawnTableIdentifiers spawnTableIdentifiers,
            IActorSpawner actorSpawner,
            IFilterContextAmenity filterContextAmenity,
            IEncounterGameObjectPlacer encounterGameObjectPlacer,
            ISpawnTableRepositoryFacade spawnTableRepositoryFacade,
            IReadOnlyMapGameObjectManager mapGameObjectManager)
        {
            _spawnTableIdentifiers = spawnTableIdentifiers;
            _actorSpawner = actorSpawner;
            _filterContextAmenity = filterContextAmenity;
            _encounterGameObjectPlacer = encounterGameObjectPlacer;
            _spawnTableRepositoryFacade = spawnTableRepositoryFacade;
            _mapGameObjectManager = mapGameObjectManager;
        }

        public int Priority => 20000;

        public void Handle(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            if (!encounter.TryGetFirst<IEncounterSpawnTableIdBehavior>(out var encounterSpawnTableIdBehavior))
            {
                return;
            }

            var spawnTableId = encounterSpawnTableIdBehavior.SpawnTableId;
            var spawnTable = _spawnTableRepositoryFacade.GetForSpawnTableId(spawnTableId);
            var spawnTableAttribute = _filterContextAmenity.CreateRequiredAttribute(
                _spawnTableIdentifiers.FilterContextSpawnTableIdentifier,
                spawnTableId);
            var spawnFilterContext = filterContext
                .WithAdditionalAttributes(new[] { spawnTableAttribute })
                .WithRange(
                    spawnTable.MinimumGenerateCount,
                    spawnTable.MaximumGenerateCount);

            var spawns = _actorSpawner
                .SpawnActors(
                    spawnFilterContext,
                    encounterSpawnTableIdBehavior.AdditionalSpawnGeneratorComponents);
            var playerControlled = _mapGameObjectManager
                .GameObjects
                .Where(x => x.Has<IPlayerControlledBehavior>());
            var actors = playerControlled
                .Concat(spawns)
                .ToArray();
            _encounterGameObjectPlacer.PlaceGameObjects(actors);
        }
    }
}
