using Autofac;
using Macerus.Plugins.Features.StatusBar.Api;
using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.StatusBar.Default.Autofac
{
    public sealed class StatusBarModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatusBarViewModel>()
                .As<IStatusBarViewModel>()
                .SingleInstance();
            builder
                .RegisterType<StatusBarController>()
                .As<IStatusBarController>()
                .SingleInstance();
        }
    }
}