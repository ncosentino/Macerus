using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class IconFilterComponentToBehaviorConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(IconFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent FilterComponent)
        {
            var iconFilterComponent = (IconFilterComponent)FilterComponent;
            yield return new HasInventoryIcon(iconFilterComponent.IconResource);
        }
    }
}
