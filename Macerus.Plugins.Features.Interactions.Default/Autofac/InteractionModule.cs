using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Interactions.Default.Autofac
{
    public sealed class InteractionModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<InteractionHandlerFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
