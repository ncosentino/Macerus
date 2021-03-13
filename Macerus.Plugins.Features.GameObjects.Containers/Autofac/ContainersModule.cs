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
                .RegisterType<ContainerIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ContainerRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .RegisterType<ContainerBehaviorsInterceptorFacade>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<ContainerBehaviorsProviderFacade>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
              .RegisterType<ContainerFactory>()
              .AsImplementedInterfaces()
              .SingleInstance();
            builder
              .RegisterType<LootDropIdentifiers>()
              .AsImplementedInterfaces()
              .SingleInstance();
            builder
              .RegisterType<LootDropFactory>()
              .AsImplementedInterfaces()
              .SingleInstance();
            builder.RegisterType<ContainerInteractableBehavior>(); // for factory
        }
    }
}