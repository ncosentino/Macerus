﻿using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Stats.Default.Autofac
{
    public sealed class StatsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatCalculationServiceAmenity>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatResourceProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}