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
                        // Test, Damaging single target
                        new SkillDefinition(
                            new StringIdentifier("test-fireball"),
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
                                    new HasSkillDisplayName("Fireball"),
                                    new HasSkillIcon(new StringIdentifier(@"graphics\skills\fireball"))),
                            },
                            new Dictionary<IIdentifier, double>() { },
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
                                    new UseInCombatSkillBehavior())
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
