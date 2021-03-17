using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Encounters.SpawnTables.Api;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api.Standard;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.SpawnTables.Implementations.Item
{
    public sealed class ActorSpawnTableFactory : IActorSpawnTableFactory
    {
        private readonly ISpawnTableIdentifiers _spawnTableIdentifiers;

        public ActorSpawnTableFactory(ISpawnTableIdentifiers spawnTableIdentifiers)
        {
            _spawnTableIdentifiers = spawnTableIdentifiers;
        }

        public IActorSpawnTable Create(
            IIdentifier spawnTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes)
        {
            var spawnTable = new ActorSpawnTable(
                spawnTableId,
                minimumGenerateCount,
                maximumGenerateCount,
                new FilterAttribute(
                    _spawnTableIdentifiers.FilterContextSpawnTableIdentifier,
                    new IdentifierFilterAttributeValue(spawnTableId),
                    false)
                    .Yield()
                    .Concat(supportedAttributes),
                providedAttributes);
            return spawnTable;
        }

        public IActorSpawnTable Create(
            IIdentifier spawnTableId,
            int minimumGenerateCount,
            int maximumGenerateCount)
        {
            var spawnTable = Create(
                spawnTableId,
                minimumGenerateCount,
                maximumGenerateCount,
                Enumerable.Empty<IFilterAttribute>(),
                Enumerable.Empty<IFilterAttribute>());
            return spawnTable;
        }
    }
}