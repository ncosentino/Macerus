using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters.SpawnTables;
using Macerus.Plugins.Features.GameObjects.Actors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Encounters.Default.StartHandlers
{
    public sealed class EncounterSpawnStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly ISpawnTableRepositoryFacade _spawnTableRepositoryFacade;
        private readonly Lazy<IReadOnlyMapGameObjectManager> _lazyMapGameObjectManager;
        private readonly Lazy<IMacerusActorIdentifiers> _lazyMacerusActorIdentifiers;
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
            Lazy<IReadOnlyMapGameObjectManager> lazyMapGameObjectManager,
            Lazy<IMacerusActorIdentifiers> lazyMacerusActorIdentifiers)
        {
            _spawnTableIdentifiers = spawnTableIdentifiers;
            _actorSpawner = actorSpawner;
            _filterContextAmenity = filterContextAmenity;
            _encounterGameObjectPlacer = encounterGameObjectPlacer;
            _spawnTableRepositoryFacade = spawnTableRepositoryFacade;
            _lazyMapGameObjectManager = lazyMapGameObjectManager;
            _lazyMacerusActorIdentifiers = lazyMacerusActorIdentifiers;
        }

        public int Priority => 20000;

        public async Task HandleAsync(
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
