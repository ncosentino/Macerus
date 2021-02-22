using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class NameFilterComponentToBehaviorConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(NameFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent FilterComponent)
        {
            var nameFilterComponent = (NameFilterComponent)FilterComponent;
            yield return new HasInventoryDisplayName(nameFilterComponent.DisplayName);
        }
    }
}
