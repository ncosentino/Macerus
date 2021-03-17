using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Api
{
    public interface ISpawnTableHandlerGenerator
    {
        IEnumerable<IGameObject> GenerateLoot(
            ISpawnTable SpawnTable,
            IFilterContext filterContext);
    }
}