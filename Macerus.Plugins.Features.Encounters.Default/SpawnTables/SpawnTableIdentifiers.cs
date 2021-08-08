using Macerus.Plugins.Features.Encounters.SpawnTables;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Encounters.Default.SpawnTables
{
    public sealed class SpawnTableIdentifiers : ISpawnTableIdentifiers
    {
        public IIdentifier FilterContextSpawnTableIdentifier { get; } = new StringIdentifier("spawn-table");
    }
}