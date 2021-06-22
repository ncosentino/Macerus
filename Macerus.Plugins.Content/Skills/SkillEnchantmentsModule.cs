using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Enchantments;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Features.Enchantments.Generation;
using ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory;
using ProjectXyz.Plugins.Features.ExpiringEnchantments;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;  // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Features.TurnBased.Default.Duration;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Skills
{
    public sealed class MagicEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var calculationPriorityFactory = c.Resolve<ICalculationPriorityFactory>();
                    var enchantmentTemplate = new EnchantmentTemplate(
                        c.Resolve<ICalculationPriorityFactory>(),
                        c.Resolve<IEnchantmentIdentifiers>());
                    var enchantmentDefinitions = new[]
                    {
                        enchantmentTemplate.CreateSkillEnchantment(
                            new StringIdentifier("green-glow-ench"),
                            new IntIdentifier(8), // green light radius
                            1,
                            1),
                        enchantmentTemplate.CreateSkillEnchantment(
                            new StringIdentifier("heal-self"),
                            new IntIdentifier(2), // life current
                            new IGeneratorComponent[]
                            {
                                new StatefulBehaviorGeneratorComponent(() =>
                                    new IBehavior[]
                                    {
                                        new EnchantmentExpressionBehavior(calculationPriorityFactory.Create<int>(1), "LIFE_CURRENT + (LIFE_MAXIMUM * 0.1 * $PER_TURN)"),
                                        new ExpiryTriggerBehavior(new DurationTriggerBehavior(5)),
                                        new AppliesToBaseStat()
                                    }),
                            }),
                        enchantmentTemplate.CreateSkillEnchantment(
                            new StringIdentifier("increase-fire-damage"),
                            new StringIdentifier("firedmg"),
                            new IGeneratorComponent[]
                            {
                                new StatefulBehaviorGeneratorComponent(() =>
                                    new IBehavior[]
                                    {
                                        new EnchantmentExpressionBehavior(calculationPriorityFactory.Create<int>(1), "FIRE_DAMAGE + 30"),
                                        new ExpiryTriggerBehavior(new DurationTriggerBehavior(5)),
                                        new AppliesToBaseStat()
                                    }),
                            }),
                        enchantmentTemplate.CreateSkillEnchantment(
                            new StringIdentifier("default-attack"),
                            new StringIdentifier("physicaldmg"),
                            new IGeneratorComponent[]
                            {
                                new StatefulBehaviorGeneratorComponent(() =>
                                    new IBehavior[]
                                    {
                                        new EnchantmentExpressionBehavior(calculationPriorityFactory.Create<int>(1), "PHYSICAL_DAMAGE + 5"),
                                        new ExpiryTriggerBehavior(new DurationTriggerBehavior(1)),
                                        new AppliesToBaseStat()
                                    }),
                            }),
                        enchantmentTemplate.CreateSkillEnchantment(
                            new StringIdentifier("increase-armor"),
                            new StringIdentifier("armor"),
                            new IGeneratorComponent[]
                            {
                                new StatefulBehaviorGeneratorComponent(() =>
                                    new IBehavior[]
                                    {
                                        new EnchantmentExpressionBehavior(calculationPriorityFactory.Create<int>(1), "ARMOR + 10"),
                                        new ExpiryTriggerBehavior(new DurationTriggerBehavior(1)),
                                        new AppliesToBaseStat()
                                    }),
                            }),
                        enchantmentTemplate.CreateSkillEnchantment(
                            new StringIdentifier("passive-rain-weight"),
                            new StringIdentifier("rain-weight"),
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new IBehavior[]
                                    {
                                        new EnchantmentExpressionBehavior(calculationPriorityFactory.Create<int>(1), "RAIN_WEIGHT * 10"),
                                    }),
                            }),
                        enchantmentTemplate.CreateSkillEnchantment(
                            new StringIdentifier("passive-rain-min"),
                            new StringIdentifier("rain-duration-minimum"),
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new IBehavior[]
                                    {
                                        new EnchantmentExpressionBehavior(calculationPriorityFactory.Create<int>(1), "RAIN_DURATION_MINIMUM * 2"),
                                    }),
                            }),
                        enchantmentTemplate.CreateSkillEnchantment(
                            new StringIdentifier("passive-rain-max"),
                            new StringIdentifier("rain-duration-maximum"),
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new IBehavior[]
                                    {
                                        new EnchantmentExpressionBehavior(calculationPriorityFactory.Create<int>(1), "RAIN_DURATION_MAXIMUM * 2.5"),
                                    }),
                            })
                    };
                    var repository = new InMemoryEnchantmentDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        enchantmentDefinitions);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }

    public sealed class EnchantmentTemplate
    {
        private readonly ICalculationPriorityFactory _calculationPriorityFactory;
        private readonly IEnchantmentIdentifiers _enchantmentIdentifiers;

        public EnchantmentTemplate(
            ICalculationPriorityFactory calculationPriorityFactory,
            IEnchantmentIdentifiers enchantmentIdentifiers)
        {
            _calculationPriorityFactory = calculationPriorityFactory;
            _enchantmentIdentifiers = enchantmentIdentifiers;
        }

        public IEnchantmentDefinition CreateSkillEnchantment(
            IIdentifier skillDefinitionId,
            IIdentifier statDefinitionId,
            double minValue,
            double maxValue)
        {
            var enchantmentDefinition = CreateSkillEnchantment(
                skillDefinitionId,
                statDefinitionId,
                new IGeneratorComponent[]
                {
                    new RandomRangeExpressionGeneratorComponent(
                        statDefinitionId,
                        "+",
                        _calculationPriorityFactory.Create<int>(1),
                        minValue, maxValue)
                });
            return enchantmentDefinition;
        }

        public IEnchantmentDefinition CreateSkillEnchantment(
            IIdentifier skillDefinitionId,
            IIdentifier statDefinitionId,
            IEnumerable<IGeneratorComponent> components)
        {
            var enchantmentDefinition = new EnchantmentDefinition(
                new[]
                {
                    new FilterAttribute(
                        _enchantmentIdentifiers.EnchantmentDefinitionId,
                        new IdentifierFilterAttributeValue(skillDefinitionId),
                        true),
                },
                new IGeneratorComponent[]
                {
                    new EnchantmentTargetGeneratorComponent(new StringIdentifier("owner")),
                    new HasStatGeneratorComponent(statDefinitionId),
                }.Concat(components));
            return enchantmentDefinition;
        }
    }
}
