using System;
using System.Collections.Generic;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Containers.Api;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerRepository :
        IContainerRepository,
        IDiscoverableGameObjectRepository
    {       
        private readonly IContainerFactory _containerFactory;
        private readonly IGameObjectIdentifiers _gameObjectIdentifiers;
        private readonly IContainerIdentifiers _containerIdentifiers;
        private readonly ContainerInteractableBehavior.Factory _containerInteractableBehaviorFactory;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IAttributeFilterer _attributeFilterer;

        public ContainerRepository(
            IContainerFactory containerFactory,
            IGameObjectIdentifiers gameObjectIdentifiers,
            IContainerIdentifiers containerIdentifiers,
            ContainerInteractableBehavior.Factory containerInteractableBehaviorFactory,
            IFilterContextAmenity filterContextAmenity,
            IAttributeFilterer attributeFilterer)
        {
            _containerFactory = containerFactory;
            _gameObjectIdentifiers = gameObjectIdentifiers;
            _containerIdentifiers = containerIdentifiers;
            _containerInteractableBehaviorFactory = containerInteractableBehaviorFactory;
            _filterContextAmenity = filterContextAmenity;
            _attributeFilterer = attributeFilterer;

            SupportedAttributes = new[]
            {
                _filterContextAmenity.CreateSupportedAttribute(
                    _gameObjectIdentifiers.FilterContextTypeId,
                    _containerIdentifiers.ContainerTypeIdentifier),
                _filterContextAmenity.CreateSupportedAlwaysMatchingAttribute(
                    _gameObjectIdentifiers.FilterContextTemplateId),
            };
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IGameObject CreateFromTemplate(
            IFilterContext filterContext,
            IReadOnlyDictionary<string, object> properties)
        {
            // FIXME: actually extend this for templates and use an attribute filterer
            var typeId = _filterContextAmenity.GetGameObjectTypeIdFromContext(filterContext);
            var templateId = _filterContextAmenity.GetGameObjectTemplateIdFromContext(filterContext);

            var containerPropertiesBehavior = new ContainerPropertiesBehavior(properties);
            var containerInteractionBehavior = _containerInteractableBehaviorFactory
                .Invoke(containerPropertiesBehavior.AutomaticInteraction);

            var additionalBehaviors = new List<IBehavior>();
            if (containerPropertiesBehavior.DropTableId != null)
            {
                additionalBehaviors.Add(new ContainerGenerateItemsBehavior(containerPropertiesBehavior.DropTableId));
            }

            var container = _containerFactory.Create(
                new TypeIdentifierBehavior()
                {
                    TypeId = typeId
                },
                new TemplateIdentifierBehavior()
                {
                    TemplateId = templateId
                },
                new IdentifierBehavior()
                {
                    Id = new StringIdentifier($"{typeId}-{templateId}-{Guid.NewGuid()}"),
                },
                new WorldLocationBehavior()
                {
                    X = containerPropertiesBehavior.X,
                    Y = containerPropertiesBehavior.Y,
                    Width = containerPropertiesBehavior.Width,
                    Height = containerPropertiesBehavior.Height,
                },
                // FIXME: support checks for things like
                // - drop table ID to use
                // - whether or not it's deposit-supported or withdrawl-only
                // - different graphics? or is that handled by the template check in the front-end?
                containerPropertiesBehavior,
                new ItemContainerBehavior(new StringIdentifier("Items")),
                containerInteractionBehavior,
                additionalBehaviors);
            return container;
        }

        public IEnumerable<IGameObject> Load(IFilterContext filterContext)
        {
            // FIXME: implement this
            yield break;
        }
    }
}
