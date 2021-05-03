using ProjectXyz.Api.GameObjects.Behaviors;
using System;
using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ISkillTargetBehavior : IBehavior
    {
        Tuple<int, int> OriginOffset { get; }

        IReadOnlyCollection<int> TeamIds { get; }

        IReadOnlyCollection<Tuple<int, int>> PatternFromOrigin { get; }
    }
}
