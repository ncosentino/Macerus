using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Game;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.Encounters.Triggers;
using Macerus.Plugins.Features.GameObjects.Containers;
using Macerus.Shared.Behaviors;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.GameObjects
{
    public class GameObjectsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<GameObjectIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => 
                {
                    var gameObjectFactory = c.Resolve<IGameObjectFactory>();
                    return new InMemoryGameObjectTemplateRepository(
                        c.Resolve<IGameObjectIdentifiers>(),
                        c.Resolve<IAttributeFilterer>(),
                        new Dictionary<IIdentifier, Func<IGameObject>>()
                        {
                            [new StringIdentifier("empty")] = () => gameObjectFactory.Create(new IBehavior[0]),
                            [new StringIdentifier("static/wall")] = () => gameObjectFactory.Create(new IBehavior[]
                            {
                                new TypeIdentifierBehavior(new StringIdentifier("static")),
                                new HasPrefabResourceIdBehavior(new StringIdentifier("Mapping/Prefabs/static/wall")),
                                new BoxColliderBehavior(0, 0, 1, 1, false),
                            }),
                            [new StringIdentifier("static/rectangulartrigger")] = () => gameObjectFactory.Create(new IBehavior[]
                            {
                                new TypeIdentifierBehavior(new StringIdentifier("static")),
                                new HasPrefabResourceIdBehavior(new StringIdentifier("Mapping/Prefabs/static/rectangulartrigger")),
                                new BoxColliderBehavior(0, 0, 0, 0, true),
                            }),
                            [new StringIdentifier("container/base")] = () => gameObjectFactory.Create(new IBehavior[]
                            {
                                new TypeIdentifierBehavior(new StringIdentifier("container")),
                                new ItemContainerBehavior(new StringIdentifier("Items")),
                                new ContainerInteractableBehavior(false, false, false), // this should always be overriden
                            }),
                            [new StringIdentifier("static/encounterspawn")] = () => gameObjectFactory.Create(new IBehavior[]
                            {
                                new TypeIdentifierBehavior(new StringIdentifier("static")),
                                new HasPrefabResourceIdBehavior(new StringIdentifier("Mapping/Prefabs/static/encounterspawn")),
                                new EncounterSpawnLocationBehavior(new int[] { }), // this should always be overriden
                            }),
                            [new StringIdentifier("static/encountertrigger")] = () => gameObjectFactory.Create(new IBehavior[]
                            {
                                new TypeIdentifierBehavior(new StringIdentifier("static")),
                                new HasPrefabResourceIdBehavior(new StringIdentifier("Mapping/Prefabs/static/rectangularencountertrigger")),
                                new EncounterTriggerPropertiesBehavior(true, null, null, 0), // this should always be overriden
                            }),
                            [new StringIdentifier("LootDrop")] = () => gameObjectFactory.Create(new IBehavior[]
                            {
                                new TypeIdentifierBehavior(new StringIdentifier("container")),
                                new HasPrefabResourceIdBehavior(new StringIdentifier("Mapping/Prefabs/Container/LootDrop")),
                                new ItemContainerBehavior(new StringIdentifier("Items")),
                                new ContainerInteractableBehavior(false, true, true),
                                new SizeBehavior(0.25, 0.25), // FIXME: why is it that 1x1 tile looks huge
                            }),
                        }); 
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
