using Macerus.Plugins.Features.GameObjects.Skills.Api;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class CombinationSkillBehavior : 
        BaseBehavior,
        ICombinationSkillBehavior
    {
        public CombinationSkillBehavior(
            params ISkillExecutorBehavior[] executorBehaviors)
        {
            SkillExecutors = executorBehaviors
                .Select(x => new SkillExecutor(x))
                .ToArray();
        }

        public IReadOnlyCollection<IGameObject> SkillExecutors { get; }
    }
}
