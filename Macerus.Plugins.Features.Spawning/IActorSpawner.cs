using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Spawning
{
    public interface IActorSpawner
    {
        IEnumerable<IGameObject> SpawnActors(
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalSpawnGeneratorComponents);
    }
}