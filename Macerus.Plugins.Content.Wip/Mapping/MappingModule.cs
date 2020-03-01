using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Content.Wip.Mapping
{
    public sealed class MappingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MapRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}