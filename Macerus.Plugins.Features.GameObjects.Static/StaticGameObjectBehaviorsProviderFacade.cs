
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Static
{
    public sealed class StaticGameObjectBehaviorsProviderFacade : IStaticGameObjectBehaviorsProviderFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableStaticGameObjectBehaviorsProvider> _providers;

        public StaticGameObjectBehaviorsProviderFacade(IEnumerable<IDiscoverableStaticGameObjectBehaviorsProvider> providers)
        {
            _providers = providers.ToArray();
        }

        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors) =>
            _providers.SelectMany(x => x.GetBehaviors(baseBehaviors));
    }
}
