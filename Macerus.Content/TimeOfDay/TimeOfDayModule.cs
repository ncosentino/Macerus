using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Content.TimeOfDay
{
    public sealed class TimeOfDayModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TimeOfDayStateIdToTermRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TimeOfDayConfiguration>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TimeOfDayConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
