using System.Collections.Generic;

namespace Macerus.Plugins.Features.Spawning.Linked
{
    public interface ILinkedSpawnTable : ISpawnTable
    {
        IReadOnlyCollection<IWeightedEntry> Entries { get; }
    }
}