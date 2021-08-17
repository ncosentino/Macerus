
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Summoning
{
    public interface ISummonSpawnTableBehavior : IBehavior
    {
        IIdentifier SpawnTableId { get; }
    }
}
