﻿using Autofac;
using Macerus.Api.GameObjects;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<AdditionalActorBehaviorsProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorBehaviorsInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();            
            builder
                .RegisterType<ActorMovementSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}