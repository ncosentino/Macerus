﻿using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Api
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