using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Death.Autofac
{
    public sealed class DeathModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ActorDeathSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DeathTriggerMechanicSource>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DeathAnimationTriggerMechanic>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}