using Autofac;
using Macerus.Plugins.Features.GameObjects.Skills.Default;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using System;
using System.Linq;

namespace Macerus.Plugins.Content.Skills
{
    public sealed class SkillModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var definitions = new[]
                    {
                        SkillDefinition
                            .FromId("default-attack")
                            .WithDisplayName("Attack")
                            .WithDisplayIcon(@"graphics\skills\default-attack")
                            .InflictDamage()
                            .CanBeUsedInCombat()
                            .StartsAtOffsetFromUser(0, 0)
                            .TargetsPattern(
                                Tuple.Create(0, 1))
                            .AffectsTeams(1)
                            .End(),
                        SkillDefinition
                            .FromId("default-defend")
                            .WithDisplayName("Defend")
                            .WithDisplayIcon(@"graphics\skills\default-defend")
                            .Enchant("increase-armor")
                            .CanBeUsedInCombat()
                            .StartsAtOffsetFromUser(0, 0)
                            .TargetsPattern(
                                Tuple.Create(0,0))
                            .AffectsTeams(0)
                            .End(),
                        SkillDefinition
                            .FromId("default-pass")
                            .WithDisplayName("Pass")
                            .WithDisplayIcon(@"graphics\skills\default-pass")
                            .CanBeUsedInCombat()
                            .End(),
                        SkillDefinition
                            .FromId("heal")
                            .WithDisplayName("Heal")
                            .WithDisplayIcon(@"graphics\skills\heal")
                            .WithResourceRequirement(4, 20)
                            .Enchant("heal-self")
                            .CanBeUsedOutOfCombat()
                            .CanBeUsedInCombat()
                            .AffectsTeams(0)
                            .StartsAtOffsetFromUser(0, 0)
                            .TargetsPattern()
                            .End(),
                        SkillDefinition
                            .FromId("fireball")
                            .WithDisplayName("Fireball")
                            .WithDisplayIcon(@"graphics\skills\fireball")
                            .WithResourceRequirement(4, 10)
                            .CanBeUsedInCombat()
                            .IsACombinationOf(
                                MacerusExecuteSkills.InParallel(
                                    SkillDefinition
                                        .Anonymous()
                                        .Enchant("increase-fire-damage")
                                        .AffectsTeams(0)
                                        .StartsAtOffsetFromUser(0, 0)
                                        .TargetsPattern()),
                                MacerusExecuteSkills.InSequence(
                                    SkillDefinition
                                        .Anonymous()
                                        .InflictDamage()
                                        .AffectsTeams(1)
                                        .StartsAtOffsetFromUser(0, 1)
                                        .TargetsPattern(
                                            Tuple.Create(0, 1))))
                            .End(),
                        SkillDefinition
                            .FromId("passive-green-glow")
                            .CanBeUsedInCombat()
                            .CanBeUsedOutOfCombat()
                            .EnchantPassive("green-glow-ench")
                            .End(),
                        SkillDefinition
                            .FromId("passive-rain")
                            .CanBeUsedInCombat()
                            .CanBeUsedOutOfCombat()
                            .EnchantPassive(
                                "passive-rain-weight",
                                "passive-rain-min",
                                "passive-rain-max")
                            .End(),
                    }.SelectMany(x => x);

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
        }
    }
}
