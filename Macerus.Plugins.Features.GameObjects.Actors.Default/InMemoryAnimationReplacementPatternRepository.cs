using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Actors.Default.Animations;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default
{
    public sealed class InMemoryAnimationReplacementPatternRepository : IAnimationReplacementPatternRepository
    {
        private readonly Dictionary<int, string> _mapping;

        public InMemoryAnimationReplacementPatternRepository(IReadOnlyDictionary<int, string> mapping)
        {
            _mapping = mapping.ToDictionary(x => x.Key, x => x.Value);
        }

        public string GetReplacementPattern(int animationReplacementStatValue)
        {
            if (!_mapping.TryGetValue(
                animationReplacementStatValue,
                out var replacementPattern))
            {
                throw new InvalidOperationException(
                    $"No replacement pattern exists for animation replacement " +
                    $"stat value {animationReplacementStatValue}.");
            }

            return replacementPattern;
        }
    }
}
