using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Mapping.Default.DataPersistence;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Content.DataPersistence.Kvp
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
                        [typeof(PostActiveMapStateKvpDataPersistenceHandler)] = int.MaxValue,
                    });
                    return loadOrder;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
