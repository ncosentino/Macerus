
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummonSpawnTableBehavior : BaseBehavior
    {
        public SummonSpawnTableBehavior(IIdentifier spawnTableId)
        {
            SpawnTableId = spawnTableId;
        }

        public IIdentifier SpawnTableId { get; }
    }
}
