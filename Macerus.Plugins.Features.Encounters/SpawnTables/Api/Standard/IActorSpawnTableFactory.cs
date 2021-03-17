using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Api.Standard
{
    public interface IActorSpawnTableFactory
    {
        IActorSpawnTable Create(
            IIdentifier SpawnTableId,
            int minimumGenerateCount,
            int maximumGenerateCount);
        
        IActorSpawnTable Create(
            IIdentifier SpawnTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes);
    }
}