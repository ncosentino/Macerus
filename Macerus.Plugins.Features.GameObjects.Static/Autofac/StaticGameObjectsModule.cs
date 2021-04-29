﻿using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Static.Autofac
{
    public sealed class StaticGameObjectsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StaticGameObjectIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StaticGameObjectTemplateRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StaticGameObjectBehaviorsInterceptorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StaticGameObjectBehaviorsProviderFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StaticGameObjectFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SpawnTemplatePropertiesBehaviorProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TriggerOnCombatEndBehaviorProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}