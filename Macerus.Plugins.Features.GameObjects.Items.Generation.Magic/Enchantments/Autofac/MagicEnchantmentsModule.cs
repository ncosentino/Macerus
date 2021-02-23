using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments.Autofac;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic
{
    public sealed class MagicEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var enchantmentTemplate = new EnchantmentTemplate(c.Resolve<ICalculationPriorityFactory>());

                    // FIXME: it's totally a smell that we have a stat
                    // definition ID per enchantment when we have things like
                    // expressions that are responsible for that stat ID.
                    // notice the double reference to a stat ID per
                    // enchantment (but i guess this is because the expression
                    // must get assigned to only one stat id).
                    // See this class for related comments:
                    // ProjectXyz.Shared.Game.GameObjects.Enchantments.EnchantmentFactory
                    var enchantmentDefinitions = new[]
                    {
                        enchantmentTemplate.CreateMagicRangeEnchantment(
                            new IntIdentifier(1), // max life
                            Affixes.Prefixes.Lively,
                            Affixes.Suffixes.OfLife,
                            1,
                            15,
                            0,
                            20),
                        enchantmentTemplate.CreateMagicRangeEnchantment(
                            new IntIdentifier(1), // max life
                            Affixes.Prefixes.Hearty,
                            Affixes.Suffixes.OfHeart,
                            16,
                            50,
                            10,
                            30),
                        enchantmentTemplate.CreateMagicRangeEnchantment(
                            new IntIdentifier(3), // max life
                            Affixes.Prefixes.Magic,
                            Affixes.Suffixes.OfMana,
                            1,
                            15,
                            0,
                            10),
                    };
                    var repository = new InMemoryEnchantmentDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        enchantmentDefinitions);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
            builder
                .RegisterType<MagicEnchantmentGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();      
        }
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
                new IFilterComponent[]
                {
                    new EnchantmentTargetFilterComponent(new StringIdentifier("self")),
                    new HasStatFilterComponent(statDefinitionId),
                    new BehaviorFilterComponent(
                        new IFilterAttribute[] { },
                        new HasPrefixBehavior(prefixId),
                        new HasSuffixBehavior(suffixId)),
                    new RandomRangeExpressionFilterComponent(
                        statDefinitionId,
                        "+",
                        _calculationPriorityFactory.Create<int>(1),
                        minValue, maxValue)
                });
            return enchantmentDefinition;
        }

        public sealed class EnchantmentDefinition : IEnchantmentDefinition
        {
            public EnchantmentDefinition(
                IEnumerable<IFilterAttribute> attributes,
                IEnumerable<IFilterComponent> filterComponents)
                : this()
            {
                SupportedAttributes = attributes.ToArray();
                FilterComponents = filterComponents.ToArray();
            }

            public EnchantmentDefinition() // serialization constructor
            {
            }

            public IEnumerable<IFilterAttribute> SupportedAttributes { get; set; }

            public IEnumerable<IFilterComponent> FilterComponents { get; set; }
        }
    }
}
