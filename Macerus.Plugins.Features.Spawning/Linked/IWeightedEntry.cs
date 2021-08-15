using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Spawning.Linked
{
    public interface IWeightedEntry
    {
        double Weight { get; }

        IIdentifier SpawnTableId { get; }
    }
}