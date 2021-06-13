using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.InGameMenu.Default.Autofac
{
    public sealed class InGameMenuModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<InGameMenuController>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InGameMenuViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}