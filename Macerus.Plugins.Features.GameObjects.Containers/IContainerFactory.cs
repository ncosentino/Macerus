﻿using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Containers.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public interface IContainerFactory
    {
        IGameObject Create(
            IReadOnlyTypeIdentifierBehavior typeIdentifierBehavior,
            IReadOnlyTemplateIdentifierBehavior templateIdentifierBehavior,
            IReadOnlyIdentifierBehavior identifierBehavior,
            IReadOnlyWorldLocationBehavior worldLocationBehavior,
            IReadOnlyContainerPropertiesBehavior propertiesBehavior,
            IItemContainerBehavior itemContainerBehavior,
            IInteractableBehavior interactableBehavior);
    }
}