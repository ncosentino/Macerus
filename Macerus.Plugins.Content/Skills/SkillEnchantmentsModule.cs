using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Content.Enchantments;
using Macerus.Plugins.Features.GameObjects.Actors.Triggers;
using Macerus.Plugins.Features.GameObjects.Enchantments;
using Macerus.Shared.Behaviors.Triggering;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Enchantments.Generation;
using ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory;
using ProjectXyz.Plugins.Features.ExpiringEnchantments;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
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
                    var hitTriggerMechanicSource = c.Resolve<Lazy<IHitTriggerMechanicSource>>();
                    var attributeFilterer = c.Resolve<Lazy<IAttributeFilterer>>();
                    var enchantmentLoader = c.Resolve<Lazy<IEnchantmentLoader>>();
                    var enchantmentDefinitionBuilder = c.Resolve<EnchantmentDefinitionBuilder>();
                    var enchantmentDefinitions = new[]
                    {
                        enchantmentDefinitionBuilder
                            .WithEnchantmentDefinitionId(new StringIdentifier("green-glow-ench"))
                            .ThatsUsedForPassiveSkill()
                            .ThatHasValueInRange(
                                new IntIdentifier(8), // green light radius
                                1,
                                1) 
                            .Build(),
                        enchantmentDefinitionBuilder
                            .WithEnchantmentDefinitionId(new StringIdentifier("heal-self"))
                            .WithStatDefinitionId(new IntIdentifier(2)) // life current
                            .ThatAppliesEffectsToSkillUser()
                            .ThatModifiesBaseStatWithExpression("LIFE_CURRENT + (LIFE_MAXIMUM * 0.1 * $PER_TURN)")
                            .ThatExpiresAfterTurns(5)
                            .Build(),
                        enchantmentDefinitionBuilder
                            .WithEnchantmentDefinitionId(new StringIdentifier("heal-self-immediate"))
                            .WithStatDefinitionId(new IntIdentifier(2)) // life current
                            .ThatAppliesEffectsToSkillUser()
                            .ThatModifiesBaseStatWithExpression("LIFE_CURRENT + 10")
                            .ThatAppliesInstantlyAsSingleUse()
                            .Build(),
                        enchantmentDefinitionBuilder
                            .WithEnchantmentDefinitionId(new StringIdentifier("increase-fire-damage"))
                            .WithStatDefinitionId(new StringIdentifier("firedmg"))
                            .ThatAppliesEffectsToSkillUser()
                            .ThatModifiesBaseStatWithExpression("FIRE_DAMAGE + 30")
                            .ThatAppliesInstantlyForAttack()
                            .Build(),
                        enchantmentTemplate.CreateSkillEnchantment(
                            new StringIdentifier("on-hit-heal"),
                            new IGeneratorComponent[]
                            {
                                new StatefulBehaviorGeneratorComponent(() =>
                                    new IBehavior[]
                                    {
                                        new ExpiryTriggerBehavior(new DurationInActionsTriggerBehavior(1)),
                                        new EnchantmentOnHitBehavior(
                                            new IFilterAttributeValue[] { new TrueAttributeFilterValue() },
                                            new IFilterAttributeValue[] { new TrueAttributeFilterValue() },
                                            new IFilterAttributeValue[] { new TrueAttributeFilterValue() },
                                            new IIdentifier[]
                                            {
                                                new StringIdentifier("heal-self-immediate"),
                                            },
                                            new IIdentifier[] { },
                                            true)
                                    }),
                            }),
                        enchantmentDefinitionBuilder
                            .WithEnchantmentDefinitionId(new StringIdentifier("default-attack"))
                            .WithStatDefinitionId(new StringIdentifier("physicaldmg"))
                            .ThatAppliesEffectsToSkillUser()
                            .ThatModifiesBaseStatWithExpression("PHYSICAL_DAMAGE + 5")
                            .ThatAppliesInstantlyForAttack()
                            .Build(),
                        enchantmentDefinitionBuilder
                            .WithEnchantmentDefinitionId(new StringIdentifier("increase-armor"))
                            .WithStatDefinitionId(new StringIdentifier("armor"))
                            .ThatAppliesEffectsToSkillUser()
                            .ThatModifiesBaseStatWithExpression("ARMOR + 10")
                            .ThatExpiresAfterTurns(1)
                            .Build(),
                        enchantmentDefinitionBuilder
                            .WithEnchantmentDefinitionId(new StringIdentifier("passive-rain-weight"))
                            .WithStatDefinitionId(new StringIdentifier("rain-weight"))
                            .ThatsUsedForPassiveSkill()
                            .ThatModifiesBaseStatWithExpression("RAIN_WEIGHT * 10")
                            .Build(),
                        enchantmentDefinitionBuilder
                            .WithEnchantmentDefinitionId(new StringIdentifier("passive-rain-min"))
                            .WithStatDefinitionId(new StringIdentifier("rain-duration-minimum"))
                            .ThatsUsedForPassiveSkill()
                            .ThatModifiesBaseStatWithExpression("RAIN_DURATION_MINIMUM * 2")
                            .Build(),
                        enchantmentDefinitionBuilder
                            .WithEnchantmentDefinitionId(new StringIdentifier("passive-rain-max"))
                            .WithStatDefinitionId(new StringIdentifier("rain-duration-maximum"))
                            .ThatsUsedForPassiveSkill()
                            .ThatModifiesBaseStatWithExpression("RAIN_DURATION_MAXIMUM * 2.5")
                            .Build(),
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
            IEnumerable<IGeneratorComponent> components) =>
            CreateSkillEnchantment(
                skillDefinitionId,
                null,
                components);

        public IEnchantmentDefinition CreateSkillEnchantment(
            IIdentifier skillDefinitionId,
            IIdentifier statDefinitionId,
            IEnumerable<IGeneratorComponent> components)
        {
            var allComponents = new List<IGeneratorComponent>(
                new IGeneratorComponent[]
                {
                    new EnchantmentTargetGeneratorComponent(new StringIdentifier("owner")),
                }.Concat(components));
            if (statDefinitionId != null)
            {
                allComponents.Add(new HasStatGeneratorComponent(statDefinitionId));
            }

            var enchantmentDefinition = new EnchantmentDefinition(
                new[]
                {
                    new FilterAttribute(
                        _enchantmentIdentifiers.EnchantmentDefinitionId,
                        new IdentifierFilterAttributeValue(skillDefinitionId),
                        true),
                },
                allComponents);
            return enchantmentDefinition;
        }
    }
}
