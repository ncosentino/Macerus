using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Spawning
{
    public interface IActorSpawnerAmenity
    {
        IEnumerable<IGameObject> SpawnActorsFromSpawnTableId(IIdentifier spawnTableId);

        IEnumerable<IGameObject> SpawnActorsFromSpawnTableId(
            IIdentifier spawnTableId,
            IFilterContext filterContext);

        IEnumerable<IGameObject> SpawnActorsFromSpawnTableId(
            IIdentifier spawnTableId,
            IFilterContext filterContext, 
            IEnumerable<IGeneratorComponent> additionalGeneratorComponents);
    }
}