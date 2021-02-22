using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments
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
