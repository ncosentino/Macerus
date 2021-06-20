using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Game.DataPersistence.Kvp.Autofac
{
    public class GameDataPersistenceModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<GameObjectKvpDataPersistenceHandler>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<RosterKvpDataPersistenceHandler>()
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }
}
