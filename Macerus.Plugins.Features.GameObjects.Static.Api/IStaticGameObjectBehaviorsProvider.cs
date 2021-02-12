using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Static.Api
{
    public interface IStaticGameObjectBehaviorsProvider
    {
        IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors);
    }
}