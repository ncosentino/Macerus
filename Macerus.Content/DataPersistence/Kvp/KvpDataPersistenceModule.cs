using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Game.DataPersistence.Kvp;
using Macerus.Plugins.Features.Mapping.Default.DataPersistence;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Content.DataPersistence.Kvp
{
    public class KvpDataPersistenceModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(x =>
                {
                    var loadOrder = new KvpDataPersistenceHandlerLoadOrder(new Dictionary<Type, int>()
                    {
                        [typeof(PreActiveMapStateKvpDataPersistenceHandler)] = int.MinValue,
                        [typeof(GameObjectKvpDataPersistenceHandler)] = int.MinValue + 1,
                        [typeof(RosterKvpDataPersistenceHandler)] = int.MaxValue - 1,
                        [typeof(PostActiveMapStateKvpDataPersistenceHandler)] = int.MaxValue,
                    });
                    return loadOrder;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
