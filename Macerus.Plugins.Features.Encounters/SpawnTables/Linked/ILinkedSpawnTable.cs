using System.Collections.Generic;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Linked
{
    public interface ILinkedSpawnTable : ISpawnTable
    {
        IReadOnlyCollection<IWeightedEntry> Entries { get; }
    }
}