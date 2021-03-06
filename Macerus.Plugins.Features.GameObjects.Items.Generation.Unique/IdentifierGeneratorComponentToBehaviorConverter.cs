﻿using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Items.Generation.Unique;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class UniqueBaseItemGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(UniqueBaseItemGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var uniqueBaseItemGeneratorComponent = (UniqueBaseItemGeneratorComponent)generatorComponent;
            yield return new UniqueBaseItemBehavior(uniqueBaseItemGeneratorComponent.Identifier);
        }
    }
}
