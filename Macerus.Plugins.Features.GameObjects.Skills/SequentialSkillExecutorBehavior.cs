using Macerus.Plugins.Features.GameObjects.Skills.Api;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SequentialSkillExecutorBehavior : 
        BaseBehavior,
        ISequentialSkillExecutorBehavior
    {
        public SequentialSkillExecutorBehavior(
            params IIdentifier[] skillIds)
        {
            SkillIds = skillIds.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> SkillIds { get; }
    }
}
