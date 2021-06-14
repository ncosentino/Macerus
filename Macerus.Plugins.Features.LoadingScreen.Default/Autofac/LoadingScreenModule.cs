using Autofac;

using Macerus.Plugins.Features.MainMenu.Default;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.LoadingScreen.Default.Autofac
{
    public sealed class LoadingScreenModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<LoadingScreenViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<LoadingScreenController>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}