using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards.Autofac
{
    public sealed class HoverCardModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<BehaviorsToHoverCardPartViewModelConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HoverCardPartViewConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NameHoverCardPartConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}