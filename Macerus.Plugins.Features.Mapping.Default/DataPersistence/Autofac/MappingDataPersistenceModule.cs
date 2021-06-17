using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Mapping.Default.DataPersistence.Autofac
{
    public sealed class MappingDataPersistenceModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<MapStateKvpDataPersistenceHandler>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<ActiveMapStateKvpDataPersistenceHandler>()
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }
}
