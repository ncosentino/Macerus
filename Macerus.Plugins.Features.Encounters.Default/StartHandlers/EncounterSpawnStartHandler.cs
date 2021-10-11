using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.Spawning;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Encounters.Default.StartHandlers
{
    public sealed class EncounterSpawnStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly Lazy<IReadOnlyMapGameObjectManager> _lazyMapGameObjectManager;
        private readonly Lazy<IMacerusActorIdentifiers> _lazyMacerusActorIdentifiers;
        private readonly IEncounterGameObjectPlacer _encounterGameObjectPlacer;
        private readonly IActorSpawnerAmenity _actorSpawnerAmenity;

        public EncounterSpawnStartHandler(
            IActorSpawnerAmenity actorSpawnerAmenity,
            IEncounterGameObjectPlacer encounterGameObjectPlacer,
            Lazy<IReadOnlyMapGameObjectManager> lazyMapGameObjectManager,
            Lazy<IMacerusActorIdentifiers> lazyMacerusActorIdentifiers)
        {
            _actorSpawnerAmenity = actorSpawnerAmenity;
            _encounterGameObjectPlacer = encounterGameObjectPlacer;
            _lazyMapGameObjectManager = lazyMapGameObjectManager;
            _lazyMacerusActorIdentifiers = lazyMacerusActorIdentifiers;
        }

        public async Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            if (!encounter.TryGetFirst<IEncounterSpawnTableIdBehavior>(out var encounterSpawnTableIdBehavior))
            {
                return;
            }

            var spawnTableId = encounterSpawnTableIdBehavior.SpawnTableId;
            
            var spawns = _actorSpawnerAmenity.SpawnActorsFromSpawnTableId(
                spawnTableId,
                filterContext,
                encounterSpawnTableIdBehavior.AdditionalSpawnGeneratorComponents);
            var mapActors = _lazyMapGameObjectManager
                .Value
                .GameObjects
                .Where(actor => Equals(
                    actor
                        .GetOnly<ITypeIdentifierBehavior>()
                        .TypeId,
                    _lazyMacerusActorIdentifiers
                        .Value
                        .ActorTypeIdentifier));
            var actors = mapActors
                .Concat(spawns)
                .ToArray();
            await _encounterGameObjectPlacer
                .PlaceGameObjectsAsync(actors)
                .ConfigureAwait(false);
        }
    }
}
