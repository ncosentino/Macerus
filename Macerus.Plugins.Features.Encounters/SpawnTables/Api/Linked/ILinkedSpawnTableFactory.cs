using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Api.Linked
{
    public interface ILinkedSpawnTableFactory
    {
        ILinkedSpawnTable Create(
            IIdentifier SpawnTableId, 
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> weightedEntries);
        
        ILinkedSpawnTable Create(
            IIdentifier SpawnTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> weightedEntries,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes);
    }
}