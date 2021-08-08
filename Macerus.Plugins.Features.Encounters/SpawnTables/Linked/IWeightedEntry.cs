using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Linked
{
    public interface IWeightedEntry
    {
        double Weight { get; }

        IIdentifier SpawnTableId { get; }
    }
}