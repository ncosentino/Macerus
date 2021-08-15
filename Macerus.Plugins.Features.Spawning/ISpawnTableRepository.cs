using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Spawning
{
    public interface ISpawnTableRepository
    {
        IEnumerable<ISpawnTable> GetAllSpawnTables();

        ISpawnTable GetForSpawnTableId(IIdentifier SpawnTableId);
    }
}