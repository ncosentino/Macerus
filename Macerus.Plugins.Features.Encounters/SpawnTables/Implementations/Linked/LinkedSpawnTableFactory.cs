using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Encounters.SpawnTables.Api;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api.Linked;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.SpawnTables.Implementations.Linked
{
    public sealed class LinkedSpawnTableFactory : ILinkedSpawnTableFactory
    {
        private readonly ISpawnTableIdentifiers _spawnTableIdentifiers;

        public LinkedSpawnTableFactory(ISpawnTableIdentifiers spawnTableIdentifiers)
        {
            _spawnTableIdentifiers = spawnTableIdentifiers;
        }

        public ILinkedSpawnTable Create(
            IIdentifier spawnTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> weightedEntries,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes)
        {
            var spawnTable = new LinkedSpawnTable(
                spawnTableId,
                minimumGenerateCount,
                maximumGenerateCount,
                weightedEntries,
                new FilterAttribute(
                    _spawnTableIdentifiers.FilterContextSpawnTableIdentifier,
                    new IdentifierFilterAttributeValue(spawnTableId),
                    false)
                    .Yield()
                    .Concat(supportedAttributes),
                providedAttributes);
            return spawnTable;
        }

        public ILinkedSpawnTable Create(
            IIdentifier spawnTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> weightedEntries)
        {
            var spawnTable = Create(
                spawnTableId,
                minimumGenerateCount,
                maximumGenerateCount,
                weightedEntries,
                Enumerable.Empty<IFilterAttribute>(),
                Enumerable.Empty<IFilterAttribute>());
            return spawnTable;
        }
    }
}