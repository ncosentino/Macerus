using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Static
{
    public sealed class StaticGameObjectBehaviorsInterceptorFacade : IStaticGameObjectBehaviorsInterceptorFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableStaticGameObjectBehaviorsInterceptor> _interceptors;

        public StaticGameObjectBehaviorsInterceptorFacade(IEnumerable<IDiscoverableStaticGameObjectBehaviorsInterceptor> interceptors)
        {
            _interceptors = interceptors.ToArray();
        }

        public void Intercept(IReadOnlyCollection<IBehavior> behaviors)
        {
            foreach (var interceptor in _interceptors)
            {
                interceptor.Intercept(behaviors);
            }
        }
    }
}
