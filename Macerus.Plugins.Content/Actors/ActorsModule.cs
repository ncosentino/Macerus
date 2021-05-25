using System.Collections.Generic;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Stats;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Actors
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
                    var hasMutableStatsBehaviorFactory = c.Resolve<IHasMutableStatsBehaviorFactory>();
                    var filterContextAmenity = c.Resolve<IFilterContextAmenity>();
                    var gameObjectIdentifiers = c.Resolve<IGameObjectIdentifiers>();
                    var actorIdentifiers = c.Resolve<IMacerusActorIdentifiers>();
                    var combatTeamIdentifiers = c.Resolve<ICombatTeamIdentifiers>();
                    var skillAmenity = c.Resolve<ISkillAmenity>();
                    var dynamicAnimationBehaviorFactory = c.Resolve<IDynamicAnimationBehaviorFactory>();

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
                                        dynamicAnimationBehaviorFactory.Create(
                                            "$actor$",
                                            new StringIdentifier(string.Empty),
                                            true,
                                            0),
                                        new PositionBehavior(0, 0),
                                        new SizeBehavior(1, 1),
                                        new MovementBehavior(),
                                        new PlayerControlledBehavior(),
                                        new AlwaysLoadWithMapBehavior(),
                                        new SkipMapSaveStateBehavior(),
                                        new ItemContainerBehavior(actorIdentifiers.InventoryIdentifier),
                                        new ItemContainerBehavior(actorIdentifiers.BeltIdentifier),
                                        new CanEquipBehavior(humanoidEquipSlotIds),
                                        new HasPrefabResourceIdBehavior(new StringIdentifier("Mapping/Prefabs/Actors/PlayerPlaceholder")),
                                    }),
                                new HasSkillsGeneratorComponent(new Dictionary<IIdentifier, int>()
                                {
                                    [new StringIdentifier("default-attack")] = 1,
                                    [new StringIdentifier("default-defend")] = 1,
                                    [new StringIdentifier("default-pass")] = 1,
                                    [new StringIdentifier("heal")] = 1,
                                    [new StringIdentifier("fireball")] = 1,
                                    [new StringIdentifier("passive-green-glow")] = 1,
                                }),
                                new HasMutableStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new IntIdentifier(1)] = 10,
                                    [new IntIdentifier(2)] = 100,
                                    [new IntIdentifier(3)] = 100,
                                    [new IntIdentifier(4)] = 100,
                                    [combatTeamIdentifiers.CombatTeamStatDefinitionId] = combatTeamIdentifiers.PlayerTeamStatValue,
                                    [new StringIdentifier("speed")] = 20, // FIXME: just for testing
                                    [new StringIdentifier("firedmg")] = 10, // FIXME: just for testing
                                    [new StringIdentifier("fireresist")] = 10, // FIXME: just for testing
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
                                        dynamicAnimationBehaviorFactory.Create(
                                            "$actor$",
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
                                    }),
                                new HasMutableStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    // FIXME: shouldn't need to rely on an override to set the animation?
                                    [new StringIdentifier("animation_override")] = 1,
                                    // resources...
                                    [new IntIdentifier(0)] = 15,
                                    [new IntIdentifier(1)] = 15,
                                    [new IntIdentifier(2)] = 10,
                                    [new IntIdentifier(3)] = 10,
                                    [new StringIdentifier("speed")] = 10, // FIXME: just for testing
                                    [new StringIdentifier("firedmg")] = 0, // FIXME: just for testing
                                    [new StringIdentifier("fireresist")] = 10, // FIXME: just for testing
                                }),
                                new HasSkillsGeneratorComponent(new Dictionary<IIdentifier, int>()
                                {
                                    [new StringIdentifier("default-attack")] = 1,
                                    [new StringIdentifier("default-defend")] = 1,
                                    [new StringIdentifier("default-pass")] = 1,
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