using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Spawning;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Spawning.Default
{
    public sealed class NoneSpawnTableRepository : IDiscoverableSpawnTableRepository
    {
        public IEnumerable<ISpawnTable> GetAllSpawnTables() =>
            Enumerable.Empty<ISpawnTable>();

        public ISpawnTable GetForSpawnTableId(IIdentifier spawnTableId) => null;
    }
}