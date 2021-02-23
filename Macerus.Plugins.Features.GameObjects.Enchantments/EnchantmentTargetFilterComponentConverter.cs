using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations; // FIXME: dependency on non-API

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class EnchantmentTargetFilterComponentConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(EnchantmentTargetFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent FilterComponent)
        {
            var enchantmentTargetFilterComponent = (EnchantmentTargetFilterComponent)FilterComponent;
            yield return new EnchantmentTargetBehavior(enchantmentTargetFilterComponent.Target);
        }
    }
}
