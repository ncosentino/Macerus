using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Stats.Autofac
{
    public sealed class StatsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatCalculationServiceAmenity>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
