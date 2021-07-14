using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors.Triggers;

using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Enchantments.Triggering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Shared.Behaviors.Triggering
{
    public sealed class EnchantmentOnHitTriggerMechanic : IHitTriggerMechanic
    {
        private readonly Lazy<IAttributeFilterer> _lazyAttributeFilterer;
        private readonly Lazy<IEnchantmentLoader> _lazyEnchantmentLoader;
        private readonly Lazy<ILogger> _lazyLogger;
        private readonly EnchantmentOnHitBehavior _enchantmentOnHitBehavior;
        private readonly RemoveEnchantmentDelegate _removeEnchantmentDelegate;

        public EnchantmentOnHitTriggerMechanic(
            Lazy<IAttributeFilterer> lazyAttributeFilterer,
            Lazy<IEnchantmentLoader> lazyEnchantmentLoader,
            Lazy<ILogger> lazyLogger,
            EnchantmentOnHitBehavior enchantmentOnHitBehavior,
            RemoveEnchantmentDelegate removeEnchantmentDelegate)
        {
            _lazyAttributeFilterer = lazyAttributeFilterer;
            _lazyEnchantmentLoader = lazyEnchantmentLoader;
            _lazyLogger = lazyLogger;
            _enchantmentOnHitBehavior = enchantmentOnHitBehavior;
            _removeEnchantmentDelegate = removeEnchantmentDelegate;
        }

        public async Task ActorHitTriggeredAsync(
            IGameObject attacker,
            IGameObject defender,
            IGameObject skill)
        {
            try
            {
                _lazyLogger.Value.Debug(
                    $"Enchantment-on-hit Trigger running filters...");

                // NOTE: probably in general want to be able to restrict this to
                // attackers that have our EnchantmentOnHitBehavior somewhere in
                // their hierarchy... but this format DOES allow you to break that
                // rule. this means we could in theory re-use this for on-hit and
                // when-hit (i.e. swapping attacker and defender). could totally
                // dream up other weird usages too.
                if (!_lazyAttributeFilterer.Value.IsMatch(attacker, _enchantmentOnHitBehavior.AttackerFilter) ||
                    !_lazyAttributeFilterer.Value.IsMatch(defender, _enchantmentOnHitBehavior.DefenderFilter) ||
                    !_lazyAttributeFilterer.Value.IsMatch(skill, _enchantmentOnHitBehavior.SkillFilter))
                {
                    _lazyLogger.Value.Debug(
                        $"Enchantment-on-hit Trigger failed filters.");
                    return;
                }

                _lazyLogger.Value.Debug(
                    $"Enchantment-on-hit Trigger passed filters.");

                var attackerEnchantments = _lazyEnchantmentLoader
                    .Value
                    .LoadForEnchantmenDefinitionIds(_enchantmentOnHitBehavior.AttackerEnchantmentDefinitionIds)
                    .ToArray();
                _lazyLogger.Value.Debug(
                    $"Enchantment-on-hit Trigger adding {attackerEnchantments.Length} enchantments to attacker.");
                attacker
                    .GetOnly<IHasEnchantmentsBehavior>()
                    .AddEnchantments(attackerEnchantments);

                var defenderEnchantments = _lazyEnchantmentLoader
                    .Value
                    .LoadForEnchantmenDefinitionIds(_enchantmentOnHitBehavior.DefenderEnchantmentDefinitionIds)
                    .ToArray();
                _lazyLogger.Value.Debug(
                    $"Enchantment-on-hit Trigger adding {defenderEnchantments.Length} enchantments to defender.");
                defender
                    .GetOnly<IHasEnchantmentsBehavior>()
                    .AddEnchantments(defenderEnchantments);
            }
            finally
            {
                if (_enchantmentOnHitBehavior.RemoveAfterTrigger)
                {
                    _removeEnchantmentDelegate.Invoke(_enchantmentOnHitBehavior.Owner);
                    _lazyLogger.Value.Debug(
                        $"Enchantment-on-hit Trigger should now be unregistered.");
                }
            }
        }
    }
}