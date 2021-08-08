using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Standard
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
            IEnumerable<IFilterAttribute> providedAttributes,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents);
    }
}