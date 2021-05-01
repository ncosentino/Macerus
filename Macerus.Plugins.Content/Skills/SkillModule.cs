using System;
using System.Collections.Generic;

using Autofac;
using Macerus.Plugins.Features.GameObjects.Skills.Default;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Skills
{
    public sealed class SkillModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var calculationPriorityFactory = c.Resolve<ICalculationPriorityFactory>();
                    var skillIdentifiers = c.Resolve<ISkillIdentifiers>();
                    var definitions = new[]
                    {
                        // Passive Enchantment, Stat-Based
                        new SkillDefinition(
                            new StringIdentifier("green-glow"),
                            new StringIdentifier("self"),
                            new IIdentifier[] { },
                            new IIdentifier[] { },
                            new Dictionary<IIdentifier, double>()
                            {
                                [new IntIdentifier(8)] = 1, // green light radius
                            },
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new PassiveSkillBehavior(),
                                    new UseInCombatSkillBehavior(),
                                    new UseOutOfCombatSkillBehavior())
                            },
                            new Dictionary<IIdentifier, double>() { },
                            skillIdentifiers),
                        // Passive Enchantment, Enchantment-Definition-Based
                        new SkillDefinition(
                            new StringIdentifier("green-glow-ench"),
                            new StringIdentifier("self"),
                            new IIdentifier[] { },
                            new IIdentifier[] { new StringIdentifier("green-glow-ench") },
                            new Dictionary<IIdentifier, double>() { },
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new PassiveSkillBehavior(),
                                    new UseInCombatSkillBehavior(),
                                    new UseOutOfCombatSkillBehavior())
                            },
                            new Dictionary<IIdentifier, double>() { },
                            skillIdentifiers),
                        // Castable, Enchantment-Definition-Based
                        new SkillDefinition(
                            new StringIdentifier("heal-self"),
                            new StringIdentifier("self"),
                            new IIdentifier[] { },
                            new IIdentifier[] { new StringIdentifier("heal-self") },
                            new Dictionary<IIdentifier, double>() { },
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new UseInCombatSkillBehavior(),
                                    new UseOutOfCombatSkillBehavior(),
                                    new EnchantTargetsBehavior(new [] {new StringIdentifier("heal-self") }),
                                    new SkillTargetBehavior(
                                        Tuple.Create(0,0),  // Starts at the caster's location
                                        new [] { 0 },       // Affects only enemy team 1
                                        new Tuple<int, int>[0]),
                                    new HasSkillDisplayName("Heal"),
                                    new HasSkillIcon(new StringIdentifier(@"graphics\skills\heal"))),
                            },
                            new Dictionary<IIdentifier, double>()
                            {
                                [new IntIdentifier(4)] = 75, // mana current
                            },
                            skillIdentifiers),
                        // Passive, Weather
                        new SkillDefinition(
                            new StringIdentifier("passive-rain"),
                            new StringIdentifier("self"),
                            new IIdentifier[] { },
                            new IIdentifier[] 
                            {
                                new StringIdentifier($"passive-rain-weight"),
                                new StringIdentifier($"passive-rain-min"),
                                new StringIdentifier($"passive-rain-max"),
                            },
                            new Dictionary<IIdentifier, double>(),
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new PassiveSkillBehavior(),
                                    new UseInCombatSkillBehavior(),
                                    new UseOutOfCombatSkillBehavior())
                            },
                            new Dictionary<IIdentifier, double>(),
                            skillIdentifiers),
                        // Passive Animation, Stat-Based
                        new SkillDefinition(
                            new StringIdentifier("skeleton-player"),
                            new StringIdentifier("self"),
                            new IIdentifier[] { },
                            new IIdentifier[] { },
                            new Dictionary<IIdentifier, double>()
                            {
                                [new StringIdentifier("animation_override")] = 1,
                                [new StringIdentifier("animation_alpha_multiplier")] = 0.5,
                            },
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new PassiveSkillBehavior(),
                                    new UseInCombatSkillBehavior(),
                                    new UseOutOfCombatSkillBehavior())
                            },
                            new Dictionary<IIdentifier, double>() { },
                            skillIdentifiers),
                        new SkillDefinition(
                            new StringIdentifier("increase-fire-damage"),
                            new StringIdentifier("self"),
                            new IIdentifier[] { },
                            new IIdentifier[] { new StringIdentifier("increase-fire-damage") },
                            new Dictionary<IIdentifier, double>() { },
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new EnchantTargetsBehavior(
                                        new StringIdentifier("increase-fire-damage")),
                                    new SkillTargetBehavior(
                                        Tuple.Create(0,0),
                                        new [] { 0 },
                                        new Tuple<int, int>[0])),
                            },
                            new Dictionary<IIdentifier, double>() { },
                            skillIdentifiers),
                        new SkillDefinition(
                            new StringIdentifier("fire-damage-line"),
                            new StringIdentifier("single-target"),
                            new IIdentifier[] { },
                            new IIdentifier[] { },
                            new Dictionary<IIdentifier, double>() { },
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new InflictDamageBehavior(),
                                    new SkillTargetBehavior(
                                        Tuple.Create(0, 0),
                                        new [] { 1 },
                                        new [] { Tuple.Create(0, 1), Tuple.Create(0, 2), Tuple.Create(0, 3)})),
                            },
                            new Dictionary<IIdentifier, double>() { },
                            skillIdentifiers),
                        new SkillDefinition(
                            new StringIdentifier("fire-damage-t"),
                            new StringIdentifier("single-target"),
                            new IIdentifier[] { },
                            new IIdentifier[] { },
                            new Dictionary<IIdentifier, double>() { },
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new InflictDamageBehavior(),
                                    new SkillTargetBehavior(
                                        Tuple.Create(1,2),
                                        new [] { 1 },
                                        new [] { Tuple.Create(0, 1), Tuple.Create(1, 1), Tuple.Create(-1, 1)})),
                            },
                            new Dictionary<IIdentifier, double>() { },
                            skillIdentifiers),
                        // Boost + damage
                        new SkillDefinition(
                            new StringIdentifier("fireball"),
                            new StringIdentifier("single-target"),
                            new IIdentifier[] { },
                            new IIdentifier[] { },
                            new Dictionary<IIdentifier, double>() { },
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new UseInCombatSkillBehavior(),
                                    new CombinationSkillBehavior(
                                        new SequentialSkillExecutorBehavior(
                                            new StringIdentifier("increase-fire-damage"),
                                            new StringIdentifier("fire-damage-line"))),
                                    new HasSkillDisplayName("Fireball"),
                                    new HasSkillIcon(new StringIdentifier(@"graphics\skills\fireball"))),
                            },
                           new Dictionary<IIdentifier, double>()
                            {
                                [new IntIdentifier(4)] = 20, // mana current
                            },
                            skillIdentifiers),
                        // Boost + damage
                        new SkillDefinition(
                            new StringIdentifier("elder-fireball"),
                            new StringIdentifier("single-target"),
                            new IIdentifier[] { },
                            new IIdentifier[] { },
                            new Dictionary<IIdentifier, double>() { },
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new UseInCombatSkillBehavior(),
                                    new CombinationSkillBehavior(
                                        new ParallelSkillExecutorBehavior(
                                            new StringIdentifier("increase-fire-damage"),
                                            new StringIdentifier("heal-self")),
                                        new SequentialSkillExecutorBehavior(
                                            new StringIdentifier("fire-damage-line"))),
                                    new HasSkillDisplayName("Elder"),
                                    new HasSkillIcon(new StringIdentifier(@"graphics\skills\elder"))),
                            },
                           new Dictionary<IIdentifier, double>()
                            {
                                [new IntIdentifier(4)] = 20, // mana current
                            },
                            skillIdentifiers),
                        // Boost + damage
                        new SkillDefinition(
                            new StringIdentifier("elder-fireball-t"),
                            new StringIdentifier("single-target"),
                            new IIdentifier[] { },
                            new IIdentifier[] { },
                            new Dictionary<IIdentifier, double>() { },
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new UseInCombatSkillBehavior(),
                                    new CombinationSkillBehavior(
                                        new ParallelSkillExecutorBehavior(
                                            new StringIdentifier("increase-fire-damage"),
                                            new StringIdentifier("heal-self")),
                                        new SequentialSkillExecutorBehavior(
                                            new StringIdentifier("fire-damage-t"))),
                                    new HasSkillDisplayName("ElderT"),
                                    new HasSkillIcon(new StringIdentifier(@"graphics\skills\elder"))),
                            },
                           new Dictionary<IIdentifier, double>()
                            {
                                [new IntIdentifier(4)] = 20, // mana current
                            },
                            skillIdentifiers),
                        // Test, Damaging single target
                        new SkillDefinition(
                            new StringIdentifier("test-attack"),
                            new StringIdentifier("single-target"),
                            new IIdentifier[] { },
                            new IIdentifier[] { },
                            new Dictionary<IIdentifier, double>() { },
                            new IFilterAttribute[] { },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new InflictDamageBehavior(),
                                    new UseInCombatSkillBehavior(),
                                    new SkillTargetBehavior(
                                        Tuple.Create(0,0),  // Starts at the caster's location
                                        new [] { 1 },       // Affects only enemy team 1
                                        new [] { Tuple.Create(0, 1) }))
                            },
                            new Dictionary<IIdentifier, double>() { },
                            skillIdentifiers),
                    };

                    var attributeFilter = c.Resolve<IAttributeFilterer>();
                    var repository = new InMemorySkillDefinitionRepository(
                        attributeFilter,
                        definitions);
                    return repository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .RegisterType<SkillAmenity>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<SkillUsage>()
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }
}
