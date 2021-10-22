using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Enchantments;

using NexusLabs.Contracts;

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
            double maxValue,
            int decimalPlaces) => CreateRangeEnchantment(
                enchantmentDefinitionId,
                statDefinitionId,
                "+",
                minValue,
                maxValue,
                decimalPlaces);


        public IEnchantmentDefinition CreateRangeEnchantment(
            IIdentifier enchantmentDefinitionId,
            IIdentifier statDefinitionId,
            string modifier,
            double minValue,
            double maxValue,
            int decimalPlaces)
        {
            Contract.Requires(
                modifier == "+" || modifier == "-" || modifier == "*",
                $"Unsupported modifier '{modifier}'. Must be +, -, or *.");

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
                    new RandomRangeExpressionGeneratorComponent(
                        statDefinitionId,
                        modifier,
                        _calculationPriorityFactory.Create<int>(1),
                        minValue,
                        maxValue,
                        decimalPlaces)
                });
            return enchantmentDefinition;
        }
    }
}
