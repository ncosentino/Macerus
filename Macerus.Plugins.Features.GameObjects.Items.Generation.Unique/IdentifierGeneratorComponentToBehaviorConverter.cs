using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Items.Generation.Unique;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class UniqueBaseItemFilterComponentToBehaviorConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(UniqueBaseItemFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent FilterComponent)
        {
            var identifierFilterComponent = (UniqueBaseItemFilterComponent)FilterComponent;
            yield return new UniqueBaseItemBehavior(identifierFilterComponent.Identifier);
        }
    }
}
