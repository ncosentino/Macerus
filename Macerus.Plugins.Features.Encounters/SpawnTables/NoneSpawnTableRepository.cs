using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Encounters.SpawnTables.Api;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.SpawnTables
{
    public sealed class NoneSpawnTableRepository : IDiscoverableSpawnTableRepository
    {
        public IEnumerable<ISpawnTable> GetAllSpawnTables() =>
            Enumerable.Empty<ISpawnTable>();

        public ISpawnTable GetForSpawnTableId(IIdentifier spawnTableId) => null;
    }
}