﻿using System;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.Summoning.Default;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Effects;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Skills
{
    public sealed class SkillModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var actorIdentifiers = c.Resolve<IMacerusActorIdentifiers>();
                    var definitions = new[]
                    {
                        SkillDefinition
                            .FromId("default-attack")
                            .WithDisplayName("Attack")
                            .WithDisplayIcon(@"graphics\skills\default-attack")
                            .WithActorAnimation(actorIdentifiers.AnimationStrike)
                            .CanBeUsedInCombat()
                            .HasEffects(
                                SkillEffectExecutors.Parallel(
                                    SkillEffectDefinition
                                        .New
                                        .Enchant("default-attack-min")
                                        .Targets(
                                            new [] { 0 },
                                            Tuple.Create(0, 0),
                                            Tuple.Create(0, 0)),
                                    SkillEffectDefinition
                                        .New
                                        .Enchant("default-attack-max")
                                        .Targets(
                                            new [] { 0 },
                                            Tuple.Create(0, 0),
                                            Tuple.Create(0, 0))),
                                SkillEffectExecutors.Sequence(
                                    SkillEffectDefinition
                                        .New
                                        .InflictDamage()
                                         .Targets(
                                            new [] { 1 },
                                            Tuple.Create(0, 0),
                                            Tuple.Create(0, 1)))),
                        SkillDefinition
                            .FromId("default-defend")
                            .WithDisplayName("Defend")
                            .WithDisplayIcon(@"graphics\skills\default-defend")
                            .WithActorAnimation(actorIdentifiers.AnimationStrike) // FIXME: wrong animation
                            .CanBeUsedInCombat()
                            .HasEffects(
                                SkillEffectExecutors.Single(
                                    SkillEffectDefinition
                                        .New
                                        .InflictDamage()
                                        .Enchant("increase-armor")
                                        .Targets(
                                            new [] { 0 },
                                            Tuple.Create(0, 0),
                                            Tuple.Create(0, 0)))),
                        SkillDefinition
                            .FromId("heal")
                            .WithDisplayName("Heal")
                            .WithDisplayIcon(@"graphics\skills\heal")
                            .WithResourceRequirement(4, 20)
                            .CanBeUsedInCombat()
                            .CanBeUsedOutOfCombat()
                            .WithActorAnimation(actorIdentifiers.AnimationCast)
                            .HasEffects(
                                SkillEffectExecutors.Single(
                                    SkillEffectDefinition
                                        .New
                                        .Enchant("heal-self")
                                        .Targets(
                                            new [] { 0 },
                                            Tuple.Create(0, 0),
                                            Tuple.Create(0, 0)))),
                        SkillDefinition
                            .FromId("fireball")
                            .WithDisplayName("Fireball")
                            .WithDisplayIcon(@"graphics\skills\fireball")
                            .WithResourceRequirement(4, 10)
                            .CanBeUsedInCombat()
                            .WithActorAnimation(actorIdentifiers.AnimationCast)
                            .HasEffects(
                                SkillEffectExecutors.Parallel(
                                    SkillEffectDefinition
                                        .New
                                        .Enchant("increase-fire-damage-min")
                                        .Targets(
                                            new [] { 0 },
                                            Tuple.Create(0, 0),
                                            Tuple.Create(0, 0)),
                                    SkillEffectDefinition
                                        .New
                                        .Enchant("increase-fire-damage-max")
                                        .Targets(
                                            new [] { 0 },
                                            Tuple.Create(0, 0),
                                            Tuple.Create(0, 0))),
                                SkillEffectExecutors.Sequence(
                                    SkillEffectDefinition
                                        .New
                                        .InflictDamage()
                                        .Targets(
                                            new [] { 1 },
                                            Tuple.Create(0, 1),
                                            Tuple.Create(0, 1),
                                            Tuple.Create(0, 2)))),
                        SkillDefinition
                            .FromId("summon-skeleton")
                            .WithDisplayName("Summon Skeleton")
                            .WithDisplayIcon(@"graphics\skills\heal")
                            .WithResourceRequirement(4, 10)
                            .CanBeUsedInCombat()
                            .CanBeUsedOutOfCombat()
                            .WithActorAnimation(actorIdentifiers.AnimationCast)
                            .HasEffects(
                                SkillEffectExecutors.Single(
                                    SkillEffectDefinition
                                        .New
                                        .Summons(new StringIdentifier("summon-skeleton-enchantment"))
                                        .Targets(
                                            new [] { 0 },
                                            Tuple.Create(0, 0),
                                            Tuple.Create(0, 1),
                                            Tuple.Create(0, -1),
                                            Tuple.Create(1, 0),
                                            Tuple.Create(1, 1),
                                            Tuple.Create(1, -1),
                                            Tuple.Create(-1, 0),
                                            Tuple.Create(-1, 1),
                                            Tuple.Create(-1, -1)))),
                        SkillDefinition
                            .FromId("passive-green-glow")
                            .WithDisplayName("Passive Green Glow")
                            .WithDisplayIcon(@"graphics\skills\heal")
                            //.CanBeUsedInCombat()
                            //.CanBeUsedOutOfCombat()
                            .HasEffects(
                                SkillEffectExecutors.Single(
                                    SkillEffectDefinition.New.EnchantPassive("green-glow-ench"))),
                        SkillDefinition
                            .FromId("passive-rain")
                            .WithDisplayName("Passive Rain")
                            .WithDisplayIcon(@"graphics\skills\heal")
                            //.CanBeUsedInCombat()
                            //.CanBeUsedOutOfCombat()
                            .HasEffects(
                                SkillEffectExecutors.Single(
                                    SkillEffectDefinition.New.EnchantPassive(
                                        "passive-rain-weight",
                                        "passive-rain-min",
                                        "passive-rain-max"))),
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
        }
    }
}
