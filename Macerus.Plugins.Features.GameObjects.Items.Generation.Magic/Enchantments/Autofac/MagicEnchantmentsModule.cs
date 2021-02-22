using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments.Autofac;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic
{
    public sealed class MagicEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
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
                        EnchantmentTemplate.CreateMagicRangeEnchantment(
                            new IntIdentifier(1), // max life
                            Affixes.Prefixes.Lively,
                            Affixes.Suffixes.OfLife,
                            1,
                            15,
                            0,
                            20),
                        EnchantmentTemplate.CreateMagicRangeEnchantment(
                            new IntIdentifier(1), // max life
                            Affixes.Prefixes.Hearty,
                            Affixes.Suffixes.OfHeart,
                            16,
                            50,
                            10,
                            30),
                        EnchantmentTemplate.CreateMagicRangeEnchantment(
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
            builder
                .RegisterType<HasPrefixFilterComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasSuffixFilterComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();            
        }
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
                    new EnchantmentTargetFilterComponent(new StringIdentifier("self")),
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
