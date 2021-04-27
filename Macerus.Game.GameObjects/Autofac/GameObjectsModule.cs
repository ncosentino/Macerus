﻿using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Game.GameObjects.Autofac
{
    public class GameObjectsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<GameObjectRepositoryAmenity>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
                .RegisterType<GameObjectTemplateRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GameObjectRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
