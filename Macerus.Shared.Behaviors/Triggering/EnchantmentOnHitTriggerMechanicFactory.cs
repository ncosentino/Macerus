using System;
using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Triggering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation;
using ProjectXyz.Plugins.Features.Triggering;

namespace Macerus.Shared.Behaviors.Triggering
{
    public sealed class EnchantmentOnHitTriggerMechanicFactory : IDiscoverableEnchantmentTriggerMechanicFactory
    {
        private readonly Lazy<IAttributeFilterer> _lazyAttributeFilterer;
        private readonly Lazy<IEnchantmentLoader> _lazyEnchantmentLoader;
        private readonly Lazy<ILogger> _lazyLogger;

        public EnchantmentOnHitTriggerMechanicFactory(
            Lazy<IAttributeFilterer> lazyAttributeFilterer,
            Lazy<IEnchantmentLoader> lazyEnchantmentLoader,
            Lazy<ILogger> lazyLogger)
        {
            _lazyAttributeFilterer = lazyAttributeFilterer;
            _lazyEnchantmentLoader = lazyEnchantmentLoader;
            _lazyLogger = lazyLogger;
        }

        public IEnumerable<ITriggerMechanic> CreateTriggerMechanicsForEnchantment(
            IGameObject enchantment,
            RemoveTriggerMechanicDelegate removeTriggerMechanicCallback,
            RemoveEnchantmentDelegate removeEnchantmentCallback)
        {
            foreach (var behavior in enchantment.Get<EnchantmentOnHitBehavior>())
            {
                var triggerMechanic = new EnchantmentOnHitTriggerMechanic(
                    _lazyAttributeFilterer,
                    _lazyEnchantmentLoader,
                    _lazyLogger,
                    behavior,
                    removeEnchantmentCallback);
                yield return triggerMechanic;
            }
        }
    }
}