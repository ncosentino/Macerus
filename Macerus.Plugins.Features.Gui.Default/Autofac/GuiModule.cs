using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Gui.Default.Autofac
{
    public sealed class GuiModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<UserInterfaceSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}