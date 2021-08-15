using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Spawning;
using Macerus.Plugins.Features.Spawning.Standard;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API

namespace Macerus.Plugins.Features.Spawning.Default.Actors
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
            IEnumerable<IFilterAttribute> providedAttributes,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents)
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
                providedAttributes,
                additionalActorGeneratorComponents);
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
                Enumerable.Empty<IFilterAttribute>(),
                Enumerable.Empty<IGeneratorComponent>());
            return spawnTable;
        }
    }
}