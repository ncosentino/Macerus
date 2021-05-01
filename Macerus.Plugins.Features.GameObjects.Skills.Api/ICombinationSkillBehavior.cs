using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ICombinationSkillBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> SkillExecutors { get; }
    }
}
