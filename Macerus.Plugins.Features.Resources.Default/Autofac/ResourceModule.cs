using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Resources.Default.Autofac
{
    public sealed class ResourceModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StringResourceRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ImageResourceRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StringResourceProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
