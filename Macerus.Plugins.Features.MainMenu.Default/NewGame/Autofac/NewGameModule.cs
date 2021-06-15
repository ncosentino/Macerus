using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.MainMenu.Default.NewGame.Autofac
{
    public sealed class NewGameModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<NewGameViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NewGameController>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}