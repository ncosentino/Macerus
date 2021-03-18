
using Macerus.Plugins.Features.Encounters.SpawnTables.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Encounters.SpawnTables
{
    public sealed class SpawnTableIdentifiers : ISpawnTableIdentifiers
    {
        public IIdentifier FilterContextSpawnTableIdentifier { get; } = new StringIdentifier("spawn-table");
    }
}