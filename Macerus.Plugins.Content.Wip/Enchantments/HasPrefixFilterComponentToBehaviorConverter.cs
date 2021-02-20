using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class HasPrefixFilterComponentToBehaviorConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(HasPrefixFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent FilterComponent)
        {
            var hasPrefixFilterComponent = (HasPrefixFilterComponent)FilterComponent;
            yield return new HasPrefixBehavior(hasPrefixFilterComponent.PrefixId);
        }
    }
}
