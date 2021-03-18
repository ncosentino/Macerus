
using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterSpawnStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly IEncounterGameObjectPlacer _encounterGameObjectPlacer;
        private readonly IActorSpawner _actorSpawner;
        private readonly IFilterContextAmenity _filterContextAmenity;

        public EncounterSpawnStartHandler(
            IActorSpawner actorSpawner,
            IFilterContextAmenity filterContextAmenity,
            IEncounterGameObjectPlacer encounterGameObjectPlacer)
        {
            _actorSpawner = actorSpawner;
            _filterContextAmenity = filterContextAmenity;
            _encounterGameObjectPlacer = encounterGameObjectPlacer;
        }

        public int Priority => 20000;

        public void Handle(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            if (!encounter.TryGetFirst<IEncounterSpawnFilterBehavior>(out var encounterSpawnFilterBehavior))
            {
                return;
            }

            var spawnContext = _filterContextAmenity.CopyWithAdditionalAttributes(
                filterContext,
                encounterSpawnFilterBehavior.ProvidedAttributes);
            var actors = _actorSpawner.SpawnActors(spawnContext);
            _encounterGameObjectPlacer.PlaceGameObjects(actors);
        }
    }
}
