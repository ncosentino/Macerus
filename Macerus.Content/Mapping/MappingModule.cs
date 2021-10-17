using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Content.Mapping
{
    public class MappingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MapIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
