using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Spawning;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Spawning.Default
{
    public sealed class SpawnTableRepositoryFacade : ISpawnTableRepositoryFacade
    {
        private readonly IReadOnlyCollection<ISpawnTableRepository> _spawnTableRepositories;

        public SpawnTableRepositoryFacade(IEnumerable<IDiscoverableSpawnTableRepository> spawnTableRepositories)
        {
            _spawnTableRepositories = spawnTableRepositories.ToArray();
        }

        public IEnumerable<ISpawnTable> GetAllSpawnTables() =>
            _spawnTableRepositories
                .SelectMany(x => x.GetAllSpawnTables());

        public ISpawnTable GetForSpawnTableId(IIdentifier spawnTableId) =>
            _spawnTableRepositories
                .Select(x => x.GetForSpawnTableId(spawnTableId))
                .FirstOrDefault(x => x != null);
    }
}