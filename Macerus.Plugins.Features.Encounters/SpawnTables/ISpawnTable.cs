using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Encounters.SpawnTables
{
    public interface ISpawnTable : IHasFilterAttributes
    {
        IIdentifier SpawnTableId { get; }

        int MinimumGenerateCount { get; }

        int MaximumGenerateCount { get; }

        IEnumerable<IFilterAttribute> ProvidedAttributes { get; }

        IReadOnlyCollection<IGeneratorComponent> ProvidedGeneratorComponents { get; }
    }
}