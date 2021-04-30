using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ICombinationSkillBehavior : IBehavior
    {
        IReadOnlyCollection<IIdentifier> SkillIds { get; }
    }
}
