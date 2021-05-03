using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ISkillExecutorBehavior : IBehavior
    {
        IReadOnlyCollection<IIdentifier> SkillIds { get; }
    }
}
