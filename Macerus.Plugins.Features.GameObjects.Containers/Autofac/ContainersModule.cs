using Autofac;

using Macerus.Plugins.Features.GameObjects.Containers.LootDrops;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Containers.Autofac
{
    public sealed class ContainersModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<LootDropFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ContainerInteractionHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
