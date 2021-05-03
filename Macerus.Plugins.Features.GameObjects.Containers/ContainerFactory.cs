using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Containers.Api;
using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerFactory : IContainerFactory
    {
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IContainerBehaviorsProviderFacade _ContainerBehaviorsProviderFacade;
        private readonly IContainerBehaviorsInterceptorFacade _ContainerBehaviorsInterceptorFacade;

        public ContainerFactory(
            IGameObjectFactory gameObjectFactory,
            IContainerBehaviorsProviderFacade ContainerBehaviorsProviderFacade,
            IContainerBehaviorsInterceptorFacade ContainerBehaviorsInterceptorFacade)
        {
            _gameObjectFactory = gameObjectFactory;
            _ContainerBehaviorsProviderFacade = ContainerBehaviorsProviderFacade;
            _ContainerBehaviorsInterceptorFacade = ContainerBehaviorsInterceptorFacade;
        }

        public IGameObject Create(
            IReadOnlyTypeIdentifierBehavior typeIdentifierBehavior,
            IReadOnlyTemplateIdentifierBehavior templateIdentifierBehavior,
            IReadOnlyIdentifierBehavior identifierBehavior,
            IReadOnlyWorldLocationBehavior worldLocationBehavior,
            IReadOnlyContainerPropertiesBehavior propertiesBehavior,
            IItemContainerBehavior itemContainerBehavior,
            IInteractableBehavior interactableBehavior,
            IReadOnlyPrefabResourceIdBehavior prefabResourceIdBehavior,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            var baseBehaviours = new IBehavior[]
            {
                typeIdentifierBehavior,
                templateIdentifierBehavior,
                identifierBehavior,
                worldLocationBehavior,
                propertiesBehavior,
                itemContainerBehavior,
                interactableBehavior,
                prefabResourceIdBehavior,
            }
            .Concat(additionalBehaviors)
            .ToArray();
            var providerBehaviors = _ContainerBehaviorsProviderFacade.GetBehaviors(baseBehaviours);
            var allBehaviors = baseBehaviours
                .Concat(providerBehaviors)
                .ToArray();
            _ContainerBehaviorsInterceptorFacade.Intercept(allBehaviors);

            var container = _gameObjectFactory.Create(allBehaviors);
            return container;
        }
    }
}
