using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Encounters.SpawnTables;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.Default.SpawnTables
{
    public sealed class NoneSpawnTableRepository : IDiscoverableSpawnTableRepository
    {
        public IEnumerable<ISpawnTable> GetAllSpawnTables() =>
            Enumerable.Empty<ISpawnTable>();

        public ISpawnTable GetForSpawnTableId(IIdentifier spawnTableId) => null;
    }
}