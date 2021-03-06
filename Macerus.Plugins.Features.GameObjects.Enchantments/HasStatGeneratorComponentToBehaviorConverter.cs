﻿using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class HasStatGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(HasStatGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var hasStatGeneratorComponent = (HasStatGeneratorComponent)generatorComponent;
            yield return new HasStatDefinitionIdBehavior()
            {
                StatDefinitionId = hasStatGeneratorComponent.StatDefinitionId,
            };
        }
    }
}
