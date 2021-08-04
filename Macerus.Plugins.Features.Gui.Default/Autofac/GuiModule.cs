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
            builder
                .RegisterType<ModalManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ModalContentConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ModalButtonViewModelFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoneModalContentPresenter>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IModalContentPresenter))
                .SingleInstance();
        }
    }
}