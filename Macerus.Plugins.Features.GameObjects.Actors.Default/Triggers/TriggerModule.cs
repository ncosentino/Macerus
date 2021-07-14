using Autofac;

using Macerus.Plugins.Features.GameObjects.Actors.Default.Triggers;
using Macerus.Plugins.Features.GameObjects.Actors.Default.Triggers.Death;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Death.Autofac
{
    public sealed class TriggerModule : SingleRegistrationModule
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
            builder
                .RegisterType<HitTriggerMechanicSource>()
                .AsImplementedInterfaces()
                .SingleInstance();            
        }
    }
}