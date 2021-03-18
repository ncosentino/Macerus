using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Encounters.SpawnTables.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.SpawnTables
{
    public sealed class InMemorySpawnTableRepository : IDiscoverableSpawnTableRepository
    {
        private readonly IReadOnlyDictionary<IIdentifier, ISpawnTable> _spawnTables;

        public InMemorySpawnTableRepository(IEnumerable<ISpawnTable> spawnTables)
        {
            _spawnTables = spawnTables.ToDictionary(
                x => x.SpawnTableId,
                x => x);
        }

        public IEnumerable<ISpawnTable> GetAllSpawnTables() => _spawnTables.Values;

        public ISpawnTable GetForSpawnTableId(IIdentifier spawnTableId)
        {
            if (!_spawnTables.TryGetValue(spawnTableId, out var match))
            {
                return null;
            }

            return match;
        }
    }
}