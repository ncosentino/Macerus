using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using Macerus.Plugins.Features.Spawning.Linked;

namespace Macerus.Plugins.Features.Spawning.Default.Linked
{
    public sealed class LinkedSpawnTable : ILinkedSpawnTable
    {
        public LinkedSpawnTable(
            IIdentifier spawnTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> entries,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes,
            IEnumerable<IGeneratorComponent> providedGeneratorComponents)
        {
            SpawnTableId = spawnTableId;
            MinimumGenerateCount = minimumGenerateCount;
            MaximumGenerateCount = maximumGenerateCount;
            Entries = entries.ToArray();
            SupportedAttributes = supportedAttributes.ToArray();
            ProvidedAttributes = providedAttributes.ToArray();
            ProvidedGeneratorComponents = providedGeneratorComponents.ToArray();
        }

        public IIdentifier SpawnTableId { get; }

        public int MinimumGenerateCount { get; }

        public int MaximumGenerateCount { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IFilterAttribute> ProvidedAttributes { get; }

        public IReadOnlyCollection<IWeightedEntry> Entries { get; }

        public IReadOnlyCollection<IGeneratorComponent> ProvidedGeneratorComponents { get; }
    }
}