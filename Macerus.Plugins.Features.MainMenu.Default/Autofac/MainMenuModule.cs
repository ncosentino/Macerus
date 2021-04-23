using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.MainMenu.Default.Autofac
{
    public sealed class MainMenuModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MainMenuViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MainMenuController>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}