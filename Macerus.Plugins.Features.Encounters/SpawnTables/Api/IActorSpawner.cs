using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Api
{
    public interface IActorSpawner
    {
        IEnumerable<IGameObject> SpawnActors(
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalSpawnGeneratorComponents);
    }
}