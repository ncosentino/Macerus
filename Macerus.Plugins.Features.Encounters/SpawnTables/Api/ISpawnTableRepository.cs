using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Api
{
    public interface ISpawnTableRepository
    {
        IEnumerable<ISpawnTable> GetAllSpawnTables();

        ISpawnTable GetForSpawnTableId(IIdentifier SpawnTableId);
    }
}