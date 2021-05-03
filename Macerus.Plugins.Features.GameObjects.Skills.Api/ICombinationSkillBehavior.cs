using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ICombinationSkillBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> SkillExecutors { get; }
    }
}
