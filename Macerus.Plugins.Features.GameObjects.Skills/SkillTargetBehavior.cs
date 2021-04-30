using ProjectXyz.Shared.Game.Behaviors;

using Macerus.Plugins.Features.GameObjects.Skills.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillTargetBehavior : 
        BaseBehavior,
        ISkillTargetBehavior
    {
        public SkillTargetBehavior(
            Tuple<int, int> originOffset,
            IEnumerable<int> teamIds,
            params Tuple<int, int>[] patternFromOrigin)
        {
            OriginOffset = originOffset;
            TeamIds = teamIds.ToArray();
            PatternFromOrigin = patternFromOrigin.ToArray();
        }

        public Tuple<int, int> OriginOffset { get; }

        public IReadOnlyCollection<int> TeamIds { get; }

        public IReadOnlyCollection<Tuple<int, int>> PatternFromOrigin { get; }
    }
}
