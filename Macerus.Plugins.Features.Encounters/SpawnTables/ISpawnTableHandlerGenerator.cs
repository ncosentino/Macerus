using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Encounters.SpawnTables
{
    public interface ISpawnTableHandlerGenerator
    {
        IEnumerable<IGameObject> GenerateActors(
            ISpawnTable SpawnTable,
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents);
    }
}