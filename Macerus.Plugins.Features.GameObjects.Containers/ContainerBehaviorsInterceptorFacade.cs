using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Containers.Api;

using ProjectXyz.Api.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerBehaviorsInterceptorFacade : IContainerBehaviorsInterceptorFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableContainerBehaviorsInterceptor> _interceptors;

        public ContainerBehaviorsInterceptorFacade(IEnumerable<IDiscoverableContainerBehaviorsInterceptor> interceptors)
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
