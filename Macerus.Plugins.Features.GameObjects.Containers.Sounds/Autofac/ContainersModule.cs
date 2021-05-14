using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Autofac
{
    public sealed class ContainersModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ContainerMakesNoiseBehaviorSerializer>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<ContainerMakesNoiseBehavior>(); // for factory
        }
    }
}
