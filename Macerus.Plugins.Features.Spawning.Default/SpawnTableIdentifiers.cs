using Macerus.Plugins.Features.Spawning;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Spawning.Default
{
    public sealed class SpawnTableIdentifiers : ISpawnTableIdentifiers
    {
        public IIdentifier FilterContextSpawnTableIdentifier { get; } = new StringIdentifier("spawn-table");
    }
}