using Macerus.Plugins.Features.GameObjects.Enchantments;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Enchantments.Generation;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Enchantments
{
    public sealed class EnchantmentTemplate
    {
        private readonly ICalculationPriorityFactory _calculationPriorityFactory;

        public EnchantmentTemplate(ICalculationPriorityFactory calculationPriorityFactory)
        {
            _calculationPriorityFactory = calculationPriorityFactory;
        }

        public IEnchantmentDefinition CreateMagicRangeEnchantment(
            IIdentifier statDefinitionId,
            IIdentifier prefixId,
            IIdentifier suffixId,
            double minValue,
            double maxValue,
            double minLevel,
            double maxLevel)
        {
            var enchantmentDefinition = new EnchantmentDefinition(
                new[]
                {
                    EnchantmentFilterAttributes.RequiresMagicAffix,
                    new FilterAttribute(
                        new StringIdentifier("item-level"),
                        new RangeFilterAttributeValue(minLevel, maxLevel),
                        true),
                },
                new IGeneratorComponent[]
                {
                    new EnchantmentTargetGeneratorComponent(new StringIdentifier("self")),
                    new HasStatGeneratorComponent(statDefinitionId),
                    new StatelessBehaviorGeneratorComponent(
                        new HasPrefixBehavior(prefixId),
                        new HasSuffixBehavior(suffixId)),
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
