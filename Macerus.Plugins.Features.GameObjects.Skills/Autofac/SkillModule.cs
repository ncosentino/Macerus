﻿using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Skills.Autofac
{
    public sealed class SkillModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var calculationPriorityFactory = c.Resolve<ICalculationPriorityFactory>();
                    var definitions = new[]
                    {
                        // Passive Enchantment, Stat-Based
                        new SkillDefinition(
                            new StringIdentifier("green-glow"),
                            new StringIdentifier("self"),
                            new IIdentifier[]
                            {
                            },
                            new Dictionary<IIdentifier, double>()
                            {
                                [new IntIdentifier(8)] = 1, // green light radius
                            },
                            new IFilterAttribute[]
                            {
                            },
                            new IFilterComponent[]
                            {
                                new BehaviorFilterComponent(
                                    new IFilterAttribute[] { },
                                    new PassiveSkillBehavior(),
                                    new UseInCombatSkillBehavior(),
                                    new UseOutOfCombatSkillBehavior())
                            },
                            new Dictionary<IIdentifier, double>() { }),
                        // Passive Enchantment, Enchantment-Definition-Based
                        new SkillDefinition(
                            new StringIdentifier("green-glow-ench"),
                            new StringIdentifier("self"),
                            new IIdentifier[]
                            {
                            },
                            new Dictionary<IIdentifier, double>()
                            {
                            },
                            new IFilterAttribute[]
                            {
                            },
                            new IFilterComponent[]
                            {
                                new BehaviorFilterComponent(
                                    new IFilterAttribute[] { },
                                    new PassiveSkillBehavior(),
                                    new UseInCombatSkillBehavior(),
                                    new UseOutOfCombatSkillBehavior())
                            },
                            new Dictionary<IIdentifier, double>() { }),
                        // Castable, Enchantment-Definition-Based
                        new SkillDefinition(
                            new StringIdentifier("heal-self"),
                            new StringIdentifier("self"),
                            new IIdentifier[]
                            {
                            },
                            new Dictionary<IIdentifier, double>()
                            {
                            },
                            new IFilterAttribute[]
                            {
                            },
                            new IFilterComponent[]
                            {
                                new BehaviorFilterComponent(
                                    new IFilterAttribute[] { },
                                    new UseInCombatSkillBehavior(),
                                    new UseOutOfCombatSkillBehavior())
                            },
                            new Dictionary<IIdentifier, double>()
                            {
                                [new IntIdentifier(4)] = 75, // mana current
                            }),
                    };

                    var attributeFilter = c.Resolve<IAttributeFilterer>();
                    var filterContextFactory = c.Resolve<IFilterContextFactory>();
                    var enchantmentLoader = c.Resolve<IEnchantmentLoader>();
                    var repository = new InMemorySkillDefinitionRepository(
                        attributeFilter,
                        definitions,
                        filterContextFactory,
                        enchantmentLoader);
                    return repository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
