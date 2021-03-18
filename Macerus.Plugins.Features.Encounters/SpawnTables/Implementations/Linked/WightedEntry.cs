using Macerus.Plugins.Features.Encounters.SpawnTables.Api.Linked;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Implementations.Linked
{
    public sealed class WightedEntry : IWeightedEntry
    {
        public WightedEntry(
            double weight,
            IIdentifier spawnTableId)
        {
            Weight = weight;
            SpawnTableId = spawnTableId;
        }

        public double Weight { get; }

        public IIdentifier SpawnTableId { get; }
    }
}