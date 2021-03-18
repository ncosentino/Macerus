﻿using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Shared.Behaviors
{
    public sealed class HasMutableStatsGeneratorComponent : IGeneratorComponent
    {
        public HasMutableStatsGeneratorComponent(IEnumerable<KeyValuePair<IIdentifier, double>> stats)
        {
            Stats = stats.ToDictionary(
                x => x.Key,
                x => x.Value);
        }

        public IReadOnlyDictionary<IIdentifier, double> Stats { get; }        
    }
}
