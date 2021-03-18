using System.Collections.Generic;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
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
                    var actorIdentifiers = c.Resolve<IActorIdentifiers>();
                    var actorDefinitions = new IActorDefinition[]
                    {
                        new ActorDefinition(
                            new IGeneratorComponent[]
                            {
                                new StatefulBehaviorGeneratorComponent(() =>
                                    new IBehavior[]
                                    {
                                        new PlayerControlledBehavior(),
                                        new ItemContainerBehavior(new StringIdentifier("Inventory")),
                                        new ItemContainerBehavior(new StringIdentifier("Belt")),
                                        new HasSkillsBehavior(),
                                        new CanEquipBehavior(new[]
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
                                        }),
                                    }),
                            },
                            new IFilterAttribute[]
                            {
                                filterContextAmenity.CreateRequiredAttribute(
                                    gameObjectIdentifiers.FilterContextTypeId,
                                    actorIdentifiers.ActorTypeIdentifier),
                                filterContextAmenity.CreateRequiredAttribute(
                                    gameObjectIdentifiers.FilterContextTemplateId,
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
                                        new HasSkillsBehavior(),
                                    }),
                                new HasMutableStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    // FIXME: shouldn't need to rely on an override to set the animation?
                                    [new StringIdentifier("animation_override")] = 1,
                                }),
                            },
                            new IFilterAttribute[]
                            {
                                filterContextAmenity.CreateRequiredAttribute(
                                    gameObjectIdentifiers.FilterContextTypeId,
                                    actorIdentifiers.ActorTypeIdentifier),
                                filterContextAmenity.CreateSupportedAttribute(
                                    gameObjectIdentifiers.FilterContextTemplateId,
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
        }
    }
}