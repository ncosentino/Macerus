using Autofac;
using Macerus.Plugins.Features.StatusBar.Api;
using Macerus.Plugins.Features.StatusBar.Default;
using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Inventory.Default.Autofac
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