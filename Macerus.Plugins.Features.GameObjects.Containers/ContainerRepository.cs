using System;
using System.Collections.Generic;
using System.Globalization;

using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Containers.Api;
using Macerus.Shared.Behaviors;

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

        public ContainerRepository(IContainerFactory containerFactory)
        {
            _containerFactory = containerFactory;
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
                    X = Convert.ToDouble(properties["X"], CultureInfo.InvariantCulture),
                    Y = Convert.ToDouble(properties["Y"], CultureInfo.InvariantCulture),
                    Width = Convert.ToDouble(properties["Width"], CultureInfo.InvariantCulture),
                    Height = Convert.ToDouble(properties["Height"], CultureInfo.InvariantCulture),
                },
                // FIXME: support checks for things like
                // - drop table ID to use
                // - whether or not it's deposit-supported or withdrawl-only
                // - different graphics? or is that handled by the template check in the front-end?
                new ContainerPropertiesBehavior(properties),
                new ItemContainerBehavior(new StringIdentifier("Items")),
                new InteractableBehavior());
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
