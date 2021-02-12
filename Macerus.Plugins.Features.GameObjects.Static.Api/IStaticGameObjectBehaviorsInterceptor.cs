using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Static.Api
{
    public interface IStaticGameObjectBehaviorsInterceptor
    {
        void Intercept(IReadOnlyCollection<IBehavior> behaviors);
    }
}