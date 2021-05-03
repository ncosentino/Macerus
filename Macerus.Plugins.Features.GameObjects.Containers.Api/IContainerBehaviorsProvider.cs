using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers.Api
{
    public interface IContainerBehaviorsProvider
    {
        IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors);
    }
}