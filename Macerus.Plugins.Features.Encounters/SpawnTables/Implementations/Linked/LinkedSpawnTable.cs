using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Encounters.SpawnTables.Api.Linked;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Implementations.Linked
{
    public sealed class LinkedSpawnTable : ILinkedSpawnTable
    {
        public LinkedSpawnTable(
            IIdentifier spawnTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> entries,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes)
        {
            SpawnTableId = spawnTableId;
            MinimumGenerateCount = minimumGenerateCount;
            MaximumGenerateCount = maximumGenerateCount;
            Entries = entries.ToArray();
            SupportedAttributes = supportedAttributes.ToArray();
            ProvidedAttributes = providedAttributes.ToArray();
        }

        public IIdentifier SpawnTableId { get; }

        public int MinimumGenerateCount { get; }

        public int MaximumGenerateCount { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IFilterAttribute> ProvidedAttributes { get; }

        public IReadOnlyCollection<IWeightedEntry> Entries { get; }
    }
}