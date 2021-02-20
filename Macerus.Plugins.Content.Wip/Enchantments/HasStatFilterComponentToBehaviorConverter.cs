using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Plugins.Features.CommonBehaviors;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class HasStatFilterComponentToBehaviorConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(HasStatFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent FilterComponent)
        {
            var hasStatFilterComponent = (HasStatFilterComponent)FilterComponent;
            yield return new HasStatDefinitionIdBehavior()
            {
                StatDefinitionId = hasStatFilterComponent.StatDefinitionId,
            };
        }
    }
}
