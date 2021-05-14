using Autofac;

using Macerus.Plugins.Features.GameObjects.Containers.LootDrops;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Content.LootDrops
{
    public sealed class LootDropsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
              .RegisterType<LootDropIdentifiers>()
              .AsImplementedInterfaces()
              .SingleInstance();
        }
    }
}
