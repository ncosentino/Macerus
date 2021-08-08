using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Linked
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
            IEnumerable<IFilterAttribute> providedAttributes,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents);
    }
}