﻿using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Containers.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerFactory : IContainerFactory
    {
        private readonly IBehaviorCollectionFactory _behaviorCollectionFactory;
        private readonly IBehaviorManager _behaviorManager;
        private readonly IContainerBehaviorsProviderFacade _ContainerBehaviorsProviderFacade;
        private readonly IContainerBehaviorsInterceptorFacade _ContainerBehaviorsInterceptorFacade;

        public ContainerFactory(
            IBehaviorCollectionFactory behaviorCollectionFactory,
            IBehaviorManager behaviorManager,
            IContainerBehaviorsProviderFacade ContainerBehaviorsProviderFacade,
            IContainerBehaviorsInterceptorFacade ContainerBehaviorsInterceptorFacade)
        {
            _behaviorCollectionFactory = behaviorCollectionFactory;
            _behaviorManager = behaviorManager;
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
            }
            .Concat(additionalBehaviors)
            .ToArray();
            var providerBehaviors = _ContainerBehaviorsProviderFacade.GetBehaviors(baseBehaviours);
            var allBehaviors = _behaviorCollectionFactory
                .Create(baseBehaviours
                .Concat(providerBehaviors));
            _ContainerBehaviorsInterceptorFacade.Intercept(allBehaviors);

            var Container = new Container(allBehaviors);
            _behaviorManager.Register(Container, allBehaviors);
            return Container;
        }
    }
}