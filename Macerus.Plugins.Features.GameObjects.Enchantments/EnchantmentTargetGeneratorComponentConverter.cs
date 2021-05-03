using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations; // FIXME: dependency on non-API

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class EnchantmentTargetGeneratorComponentConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(EnchantmentTargetGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var enchantmentTargetGeneratorComponent = (EnchantmentTargetGeneratorComponent)generatorComponent;
            yield return new EnchantmentTargetBehavior(enchantmentTargetGeneratorComponent.Target);
        }
    }
}
