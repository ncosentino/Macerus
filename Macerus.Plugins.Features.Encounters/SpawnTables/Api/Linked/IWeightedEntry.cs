using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Api.Linked
{
    public interface IWeightedEntry
    {
        double Weight { get; }

        IIdentifier SpawnTableId { get; }
    }
}