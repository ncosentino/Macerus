using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class HasSuffixFilterComponentToBehaviorConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(HasSuffixFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent FilterComponent)
        {
            var hasSuffixFilterComponent = (HasSuffixFilterComponent)FilterComponent;
            yield return new HasSuffixBehavior(hasSuffixFilterComponent.SuffixId);
        }
    }
}
