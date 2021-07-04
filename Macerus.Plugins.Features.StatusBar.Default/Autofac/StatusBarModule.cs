using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.StatusBar.Default.Autofac
{
    public sealed class StatusBarModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatusBarStringProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatusBarViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatusBarController>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}