using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.DataPersistence.Default.Autofac
{
    public sealed class DataPersistenceModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<DataPersistenceManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DataPersistenceHandlerFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
