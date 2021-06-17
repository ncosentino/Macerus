using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.DataPersistence.Kvp.InMemory.Autofac
{
    public sealed class InMemoryKvpDataPersistenceModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<InMemoryKvpDataStore>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InMemoryKvpDataStoreManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<KvpDataPersistenceHandlerFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoneHandlerLoadOrder>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IKvpDataPersistenceHandlerLoadOrder))
                .SingleInstance();
        }
    }
}
