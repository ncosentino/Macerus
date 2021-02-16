using System;
using System.Collections.Generic;

using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Containers.Api;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerRepository :
        IContainerRepository,
        IDiscoverableGameObjectRepository
    {
        private static readonly IIdentifier CONTAINER_TYPE_ID = new StringIdentifier("container");
        
        private readonly IContainerFactory _containerFactory;
        private readonly ContainerInteractableBehavior.Factory _containerInteractableBehaviorFactory;

        public ContainerRepository(
            IContainerFactory containerFactory,
            ContainerInteractableBehavior.Factory containerInteractableBehaviorFactory)
        {
            _containerFactory = containerFactory;
            _containerInteractableBehaviorFactory = containerInteractableBehaviorFactory;
        }

        public static IIdentifier ContainerTypeId => CONTAINER_TYPE_ID;

        public bool CanCreateFromTemplate(
            IIdentifier typeId,
            IIdentifier templateId)
        {
            var canCreateFromTemplate = typeId.Equals(CONTAINER_TYPE_ID) && templateId is StringIdentifier;
            return canCreateFromTemplate;
        }

        public bool CanLoad(
            IIdentifier typeId,
            IIdentifier objectId) => false;

        public IGameObject CreateFromTemplate(
            IIdentifier typeId,
            IIdentifier templateId,
            IReadOnlyDictionary<string, object> properties)
        {
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

        public IGameObject Load(
            IIdentifier typeId,
            IIdentifier objectId)
        {
            throw new NotSupportedException(
                $"'{GetType()}' does not support '{nameof(Load)}'.");
        }
    }
}
