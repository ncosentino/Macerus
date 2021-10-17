using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Enchantments;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.Default;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Enchantments
{
    public sealed class EnchantmentTemplate
    {
        private readonly ICalculationPriorityFactory _calculationPriorityFactory;
        private readonly IEnchantmentIdentifiers _enchantmentIdentifiers;
        private readonly IFilterContextAmenity _filterContextAmenity;

        public EnchantmentTemplate(
            ICalculationPriorityFactory calculationPriorityFactory,
            IEnchantmentIdentifiers enchantmentIdentifiers,
            IFilterContextAmenity filterContextAmenity)
        {
            _calculationPriorityFactory = calculationPriorityFactory;
            _enchantmentIdentifiers = enchantmentIdentifiers;
            _filterContextAmenity = filterContextAmenity;
        }

        public IEnchantmentDefinition CreateRangeEnchantment(
            IIdentifier enchantmentDefinitionId,
            IIdentifier statDefinitionId,
            double minValue,
            double maxValue)
        {
            var enchantmentDefinition = new EnchantmentDefinition(
                new[]
                {
                    _filterContextAmenity.CreateRequiredAttribute(
                        _enchantmentIdentifiers.EnchantmentDefinitionId,
                        enchantmentDefinitionId),
                },
                new IGeneratorComponent[]
                {
                    new EnchantmentTargetGeneratorComponent(new StringIdentifier("self")),
                    new HasStatGeneratorComponent(statDefinitionId),
                    //new StatelessBehaviorGeneratorComponent(
                    //    new HasPrefixBehavior(prefixStringResourceId),
                    //    new HasSuffixBehavior(suffixStringResourceId)),
                    new RandomRangeExpressionGeneratorComponent(
                        statDefinitionId,
                        "+",
                        _calculationPriorityFactory.Create<int>(1),
                        minValue, maxValue)
                });
            return enchantmentDefinition;
        }
    }
}
