using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Inventory.Default.HoverCards;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Content.Items
{
    public sealed class ItemsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ItemIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .Register(x =>
               {
                   var loadOrder = new HoverCardPartConverterLoadOrder(new Dictionary<Type, int>()
                   {
                       [typeof(NameHoverCardPartConverter)] = int.MinValue,
                       [typeof(BaseStatsHoverCardPartConverter)] = int.MinValue + 1,
                   });
                   return loadOrder;
               })
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }
}
