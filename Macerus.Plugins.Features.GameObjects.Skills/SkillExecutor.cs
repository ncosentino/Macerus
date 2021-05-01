using Macerus.Plugins.Features.GameObjects.Skills.Api;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillExecutor : IGameObject
    {
        public SkillExecutor(ISkillExecutorBehavior skillExecutorBehavior)
        {
            Behaviors = new[] { skillExecutorBehavior };
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}
