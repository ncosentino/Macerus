using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Enchantments;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments.Autofac;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.Enchantments.Generation.MySql;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Items
{
    public sealed class EnchantmentTests
    {
        private static readonly MacerusContainer _container;

        static EnchantmentTests()
        {
            _container = new MacerusContainer();
        }

        [Fact(Skip = "FIXME: this was helpful to prove things out... transform this into validation for what exists")]
        public void EnchantmentDefinitions_WriteToRepository_ReadBackFromRepository()
        {
            var statDefinitionToTermMappingRepositories = _container.Resolve<IEnumerable<IStatDefinitionToTermMappingRepository>>();
            var statDefinitionTermMapping = statDefinitionToTermMappingRepositories
                .SelectMany(x => x.GetStatDefinitionIdToTermMappings())
                .ToDictionary(
                    x => x.Term,
                    x => x);
            var enchantmentDefinitions = new[]
            {
                EnchantmentTemplate.CreateMagicRangeEnchantment(
                    statDefinitionTermMapping["LIFE_MAXIMUM"].StatDefinitionId,
                    Affixes.Prefixes.Lively,
                    Affixes.Suffixes.OfLife,
                    1,
                    15,
                    0,
                    20),
                EnchantmentTemplate.CreateMagicRangeEnchantment(
                    statDefinitionTermMapping["LIFE_MAXIMUM"].StatDefinitionId,
                    Affixes.Prefixes.Hearty,
                    Affixes.Suffixes.OfHeart,
                    16,
                    50,
                    10,
                    30),
                EnchantmentTemplate.CreateMagicRangeEnchantment(
                    statDefinitionTermMapping["MANA_MAXIMUM"].StatDefinitionId,
                    Affixes.Prefixes.Magic,
                    Affixes.Suffixes.OfMana,
                    1,
                    15,
                    0,
                    10),
            };
            
            var enchantmentDefinitionRepository = _container.Resolve<IEnchantmentDefinitionRepository>();
            var filterContextProvider = _container.Resolve<IFilterContextProvider>();
            
            enchantmentDefinitionRepository.WriteEnchantmentDefinitions(enchantmentDefinitions);
            var results = enchantmentDefinitionRepository
                .ReadEnchantmentDefinitions(filterContextProvider.GetContext())
                .ToArray();
        }

        public static class EnchantmentTemplate
        {
            public static IEnchantmentDefinition CreateMagicRangeEnchantment(
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
                    new IFilterComponent[]
                    {
                    new HasStatFilterComponent(statDefinitionId),
                    new HasPrefixFilterComponent(prefixId),
                    new HasSuffixFilterComponent(suffixId),
                    new RandomRangeExpressionFilterComponent(
                        statDefinitionId,
                        "+",
                        new CalculationPriority<int>(1),
                        minValue, maxValue)
                    });
                return enchantmentDefinition;
            }
        }
    }
}
