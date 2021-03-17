using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Weather.Autofac
{
    public sealed class WeatherModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<WeatherIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherModifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherAmenity>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
