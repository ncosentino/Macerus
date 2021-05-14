using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Mapping.Default.Autofac
{
    public sealed class MappingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<MapResourceIdConverter>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<MapRepository>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<MapGameObjectRepository>()
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }
}
