using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Encounters.SpawnTables.Api.Standard;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.SpawnTables.Implementations.Item
{
    public sealed class ActorSpawnTable : IActorSpawnTable
    {
        public ActorSpawnTable(
            IIdentifier spawnTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes)
        {
            SpawnTableId = spawnTableId;
            MinimumGenerateCount = minimumGenerateCount;
            MaximumGenerateCount = maximumGenerateCount;
            SupportedAttributes = supportedAttributes.ToArray();
            ProvidedAttributes = providedAttributes.ToArray();
        }

        public IIdentifier SpawnTableId { get; }

        public int MinimumGenerateCount { get; }

        public int MaximumGenerateCount { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IFilterAttribute> ProvidedAttributes { get; }
    }
}