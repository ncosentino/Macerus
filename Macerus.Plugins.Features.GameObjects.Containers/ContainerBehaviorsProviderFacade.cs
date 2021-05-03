
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Containers.Api;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerBehaviorsProviderFacade : IContainerBehaviorsProviderFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableContainerBehaviorsProvider> _providers;

        public ContainerBehaviorsProviderFacade(IEnumerable<IDiscoverableContainerBehaviorsProvider> providers)
        {
            _providers = providers.ToArray();
        }

        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors) =>
            _providers.SelectMany(x => x.GetBehaviors(baseBehaviors));
    }
}
