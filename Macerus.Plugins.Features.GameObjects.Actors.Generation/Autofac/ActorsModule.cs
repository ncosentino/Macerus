using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation.Autofac
{
    public sealed class ActorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ActorDefinitionRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NormalActorGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorGeneratorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
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
                                new FilterAttribute(
                                    gameObjectIdentifiers.FilterContextTypeId,
                                    new IdentifierFilterAttributeValue(actorIdentifiers.ActorTypeIdentifier),
                                    true),
                                new FilterAttribute(
                                    gameObjectIdentifiers.FilterContextTemplateId,
                                    new IdentifierFilterAttributeValue(new StringIdentifier("player")),
                                    true),
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