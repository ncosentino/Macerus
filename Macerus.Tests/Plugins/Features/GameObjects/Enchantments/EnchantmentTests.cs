using System.Linq;

using Macerus.Plugins.Content.Enchantments;
using Macerus.Plugins.Features.GameObjects.Enchantments;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.Default;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Features.Stats;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Enchantments
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
            var enchantmentTemplate = new EnchantmentTemplate(_container.Resolve<ICalculationPriorityFactory>());
            var statDefinitionToTermMappingRepository = _container.Resolve<IReadOnlyStatDefinitionToTermMappingRepositoryFacade>();
            var statDefinitionTermMapping = statDefinitionToTermMappingRepository
                .GetStatDefinitionIdToTermMappings()
                .ToDictionary(
                    x => x.Term,
                    x => x);
            var enchantmentDefinitions = new[]
            {
                enchantmentTemplate.CreateMagicRangeEnchantment(
                    statDefinitionTermMapping["LIFE_MAXIMUM"].StatDefinitionId,
                    Affixes.Prefixes.Lively,
                    Affixes.Suffixes.OfLife,
                    1,
                    15,
                    0,
                    20),
                enchantmentTemplate.CreateMagicRangeEnchantment(
                    statDefinitionTermMapping["LIFE_MAXIMUM"].StatDefinitionId,
                    Affixes.Prefixes.Hearty,
                    Affixes.Suffixes.OfHeart,
                    16,
                    50,
                    10,
                    30),
                enchantmentTemplate.CreateMagicRangeEnchantment(
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
                        new HasStatGeneratorComponent(statDefinitionId),
                        new StatelessBehaviorGeneratorComponent(
                            new IBehavior[]
                            {
                                new HasPrefixBehavior(prefixId),
                                new HasSuffixBehavior(suffixId),
                            }),
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
}
