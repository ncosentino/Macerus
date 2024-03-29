﻿using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Actors.Default;
using Macerus.Plugins.Features.GameObjects.Actors.Default.Animations;
using Macerus.Plugins.Features.GameObjects.Actors.Default.Interactions;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;
using Macerus.Plugins.Features.GameObjects.Skills;
using Macerus.Plugins.Features.Summoning;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Actors;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Features.Mapping.Default;
using ProjectXyz.Plugins.Features.PartyManagement.Default;
using ProjectXyz.Plugins.Features.Stats.Default;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Actors
{
    public sealed class ActorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var dynamicAnimationIdentifiers = c.Resolve<IDynamicAnimationIdentifiers>();
                    var mapping = new Dictionary<IIdentifier, string>();
                    foreach (var animationStatId in DynamicAnimationIdentifiers.GetAllStatDefinitionIds(dynamicAnimationIdentifiers))
                    {
                        mapping.Add(
                            animationStatId,
                            $"{animationStatId.ToString().ToUpperInvariant()}");
                    }

                    return new InMemoryStatDefinitionToTermMappingRepository(mapping);
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var mapping = new Dictionary<int, string>()
                    {
                        [0] = "player_male",
                        [1] = "skeleton_basic",
                    };

                    return new InMemoryAnimationReplacementPatternRepository(mapping);
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var hasStatsBehaviorFactory = c.Resolve<IHasStatsBehaviorFactory>();
                    var filterContextAmenity = c.Resolve<IFilterContextAmenity>();
                    var gameObjectIdentifiers = c.Resolve<IGameObjectIdentifiers>();
                    var actorIdentifiers = c.Resolve<IMacerusActorIdentifiers>();
                    var combatTeamIdentifiers = c.Resolve<ICombatTeamIdentifiers>();
                    var combatStatIdentifiers = c.Resolve<ICombatStatIdentifiers>();
                    var skillAmenity = c.Resolve<ISkillAmenity>();
                    var dynamicAnimationBehaviorFactory = c.Resolve<IDynamicAnimationBehaviorFactory>();
                    var summoningBehaviorFactory = c.Resolve<ISummoningBehaviorFactory>();

                    IReadOnlyCollection<IIdentifier> humanoidEquipSlotIds = new[]
                    {
                        new StringIdentifier("head"),
                        new StringIdentifier("body"),
                        new StringIdentifier("left hand"),
                        new StringIdentifier("right hand"),
                        new StringIdentifier("amulet"),
                        new StringIdentifier("ring1"),
                        new StringIdentifier("ring2"),
                        new StringIdentifier("shoulders"),
                        new StringIdentifier("hands"),
                        new StringIdentifier("waist"),
                        new StringIdentifier("feet"),
                        new StringIdentifier("legs"),
                        new StringIdentifier("back"),
                    };

                    var actorDefinitions = new IActorDefinition[]
                    {
                        new ActorDefinition(
                            new IGeneratorComponent[]
                            {
                                new StatefulBehaviorGeneratorComponent(() =>
                                    new IBehavior[]
                                    {
                                        // FIXME: this is a CRUTCH and we should not support it because it completely breaks with party-support
                                        new IdentifierBehavior(new StringIdentifier("player")),
                                        new HasDisplayNameBehavior("Player"),
                                        dynamicAnimationBehaviorFactory.Create(
                                            new StringIdentifier(string.Empty),
                                            true,
                                            0),
                                        new PositionBehavior(0, 0),
                                        new SizeBehavior(1, 1),
                                        new MovementBehavior(),
                                        new PlayerControlledBehavior(),
                                        new ActorInteractablesBehavior(),
                                        new RosterBehavior(),
                                        new SkipMapSaveStateBehavior(),
                                        new ItemContainerBehavior(actorIdentifiers.InventoryIdentifier),
                                        new ItemContainerBehavior(actorIdentifiers.CraftingInventoryIdentifier),
                                        new ItemContainerBehavior(actorIdentifiers.BeltIdentifier),
                                        new CanEquipBehavior(humanoidEquipSlotIds),
                                        new HasPrefabResourceIdBehavior(new StringIdentifier("Mapping/Prefabs/Actors/PlayerPlaceholder")),
                                        new HasDisplayIconBehavior(new StringIdentifier("graphics/actors/portraits/do-not-distribute/test-player-male")),
                                        summoningBehaviorFactory.Create(),
                                    }),
                                new HasSkillsGeneratorComponent(new Dictionary<IIdentifier, int>()
                                {
                                    [new StringIdentifier("default-attack")] = 1,
                                    [new StringIdentifier("default-defend")] = 1,
                                    [new StringIdentifier("heal")] = 1,
                                    [new StringIdentifier("fireball")] = 1,
                                    [new StringIdentifier("passive-green-glow")] = 1,
                                    [new StringIdentifier("summon-skeleton")] = 1,
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_1")] = 10,
                                    [new StringIdentifier("stat_2")] = 100,
                                    [new StringIdentifier("stat_3")] = 100,
                                    [new StringIdentifier("stat_4")] = 100,
                                    [combatTeamIdentifiers.CombatTeamStatDefinitionId] = combatTeamIdentifiers.PlayerTeamStatValue,
                                    [combatStatIdentifiers.TurnSpeedStatId] = 20, // FIXME: just for testing
                                    [combatStatIdentifiers.AttackSpeedStatId] = 1.1, // FIXME: just for testing
                                    [combatStatIdentifiers.FireDamageMinStatId] = 10, // FIXME: just for testing
                                    [combatStatIdentifiers.FireDamageMaxStatId] = 15, // FIXME: just for testing
                                    [combatStatIdentifiers.FireResistStatId] = 10, // FIXME: just for testing
                                    [actorIdentifiers.MoveDistancePerTurnTotalStatDefinitionId] = 3, // FIXME: just for testing
                                    [actorIdentifiers.MoveDiagonallyStatDefinitionId] = 0, // FIXME: just for testing
                                    [actorIdentifiers.LevelStatDefinitionId] = 1,
                                    [actorIdentifiers.ExperienceForNextLevelStatDefinitionId] = 100, // FIXME: just for testing
                                    [new StringIdentifier("maximum_summon_skeletons")] = 2, // FIXME: just for testing

                                }),
                            },
                            new IFilterAttribute[]
                            {
                                filterContextAmenity.CreateRequiredAttribute(
                                    gameObjectIdentifiers.FilterContextTypeId,
                                    actorIdentifiers.ActorTypeIdentifier),
                                filterContextAmenity.CreateRequiredAttribute(
                                    actorIdentifiers.ActorDefinitionIdentifier,
                                    new StringIdentifier("player")),
                                filterContextAmenity.CreateSupportedAttribute(
                                    new StringIdentifier("affix-type"),
                                    "normal"),
                            }),
                        new ActorDefinition(
                            new IGeneratorComponent[]
                            {
                                new StatefulBehaviorGeneratorComponent(() =>
                                    new IBehavior[]
                                    {
                                        new IdentifierBehavior(new StringIdentifier(Guid.NewGuid().ToString())),
                                        new HasDisplayNameBehavior("Hired Help"),
                                        dynamicAnimationBehaviorFactory.Create(
                                            new StringIdentifier(string.Empty),
                                            true,
                                            0),
                                        new PositionBehavior(0, 0),
                                        new SizeBehavior(1, 1),
                                        new MovementBehavior(),
                                        new RosterBehavior(),
                                        new PlayerControlledBehavior(),
                                        new ActorInteractablesBehavior(),
                                        new SkipMapSaveStateBehavior(),
                                        new ItemContainerBehavior(actorIdentifiers.InventoryIdentifier),
                                        new CanEquipBehavior(humanoidEquipSlotIds),
                                        new HasPrefabResourceIdBehavior(new StringIdentifier("Mapping/Prefabs/Actors/PlayerPlaceholder")),
                                        new HasDisplayIconBehavior(new StringIdentifier("graphics/actors/portraits/do-not-distribute/test-player-male")),
                                        summoningBehaviorFactory.Create(),
                                    }),
                                new HasSkillsGeneratorComponent(new Dictionary<IIdentifier, int>()
                                {
                                    [new StringIdentifier("default-attack")] = 1,
                                    [new StringIdentifier("default-defend")] = 1,
                                    [new StringIdentifier("fireball")] = 1,
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_1")] = 10,
                                    [new StringIdentifier("stat_2")] = 100,
                                    [new StringIdentifier("stat_3")] = 100,
                                    [new StringIdentifier("stat_4")] = 100,
                                    [combatTeamIdentifiers.CombatTeamStatDefinitionId] = combatTeamIdentifiers.PlayerTeamStatValue,
                                    [combatStatIdentifiers.TurnSpeedStatId] = 20, // FIXME: just for testing
                                    [combatStatIdentifiers.AttackSpeedStatId] = 1, // FIXME: just for testing
                                    [combatStatIdentifiers.FireDamageMinStatId] = 8, // FIXME: just for testing
                                    [combatStatIdentifiers.FireDamageMaxStatId] = 12, // FIXME: just for testing
                                    [combatStatIdentifiers.FireResistStatId] = 10, // FIXME: just for testing
                                    [actorIdentifiers.MoveDistancePerTurnTotalStatDefinitionId] = 3, // FIXME: just for testing
                                    [actorIdentifiers.MoveDiagonallyStatDefinitionId] = 0, // FIXME: just for testing
                                    [actorIdentifiers.LevelStatDefinitionId] = 1,
                                    [actorIdentifiers.ExperienceForNextLevelStatDefinitionId] = 100, // FIXME: just for testing
                                    [new StringIdentifier("maximum_summon_skeletons")] = 2, // FIXME: just for testing
                                }),
                            },
                            new IFilterAttribute[]
                            {
                                filterContextAmenity.CreateRequiredAttribute(
                                    gameObjectIdentifiers.FilterContextTypeId,
                                    actorIdentifiers.ActorTypeIdentifier),
                                filterContextAmenity.CreateRequiredAttribute(
                                    actorIdentifiers.ActorDefinitionIdentifier,
                                    new StringIdentifier("test-mercenary")),
                                filterContextAmenity.CreateSupportedAttribute(
                                    new StringIdentifier("affix-type"),
                                    "normal"),
                            }),
                        new ActorDefinition(
                            new IGeneratorComponent[]
                            {
                                new StatefulBehaviorGeneratorComponent(() =>
                                    new IBehavior[]
                                    {
                                        new HasDisplayNameBehavior("Skeleton"),
                                        dynamicAnimationBehaviorFactory.Create(
                                            new StringIdentifier(string.Empty),
                                            true,
                                            0),
                                        new PositionBehavior(0, 0),
                                        new SizeBehavior(1, 1),
                                        new MovementBehavior(),
                                        new CombatAIBehavior(),
                                        new ItemContainerBehavior(actorIdentifiers.InventoryIdentifier),
                                        new CanEquipBehavior(humanoidEquipSlotIds),
                                        new CorpseInteractableBehavior(false),
                                        new HasPrefabResourceIdBehavior(new StringIdentifier("Mapping/Prefabs/Actors/Actor")),
                                        new HasDisplayIconBehavior(new StringIdentifier("graphics/actors/portraits/do-not-distribute/test-skeleton")),
                                    }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    // FIXME: shouldn't need to rely on an override to set the animation?
                                    [new StringIdentifier("animation_override")] = 1,
                                    // resources...
                                    [new StringIdentifier("stat_1")] = 15,
                                    [new StringIdentifier("stat_2")] = 15,
                                    [new StringIdentifier("stat_3")] = 10,
                                    [new StringIdentifier("stat_4")] = 10,
                                    [combatStatIdentifiers.TurnSpeedStatId] = 10, // FIXME: just for testing
                                    [combatStatIdentifiers.AttackSpeedStatId] = 0.8, // FIXME: just for testing
                                    [combatStatIdentifiers.FireDamageMinStatId] = 0, // FIXME: just for testing
                                    [combatStatIdentifiers.FireDamageMaxStatId] = 0, // FIXME: just for testing
                                    [combatStatIdentifiers.FireResistStatId] = 10, // FIXME: just for testing
                                    [actorIdentifiers.MoveDistancePerTurnTotalStatDefinitionId] = 4, // FIXME: just for testing
                                    [actorIdentifiers.MoveDiagonallyStatDefinitionId] = 1, // FIXME: just for testing
                                }),
                                new HasSkillsGeneratorComponent(new Dictionary<IIdentifier, int>()
                                {
                                    [new StringIdentifier("default-attack")] = 1,
                                    [new StringIdentifier("default-defend")] = 1,
                                }),
                                new SpawnWithItemsGeneratorComponent(new StringIdentifier("test-skeleton-drop")),
                                new SpawnWithEquipmentGeneratorComponent(
                                    new StringIdentifier("test-skeleton-drop"),
                                    true,
                                    null),
                            },
                            new IFilterAttribute[]
                            {
                                filterContextAmenity.CreateRequiredAttribute(
                                    gameObjectIdentifiers.FilterContextTypeId,
                                    actorIdentifiers.ActorTypeIdentifier),
                                filterContextAmenity.CreateSupportedAttribute(
                                    actorIdentifiers.ActorDefinitionIdentifier,
                                    new StringIdentifier("test-skeleton")),
                                filterContextAmenity.CreateRequiredAttributeForAny(
                                    new StringIdentifier("affix-type"),
                                    "normal",
                                    "enhanced"),
                            }),
                    };

                    var attributeFilterer = c.Resolve<IAttributeFilterer>();
                    return new InMemoryActorDefinitionRepository(
                        attributeFilterer,
                        actorDefinitions);
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}