using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class NameGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(NameGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var nameGeneratorComponent = (NameGeneratorComponent)generatorComponent;
            yield return new HasInventoryDisplayName(nameGeneratorComponent.DisplayName);
        }
    }
}
