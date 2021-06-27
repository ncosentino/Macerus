using Autofac;

using Macerus.Plugins.Features.GameObjects.Actors.LightRadius;
using Macerus.Plugins.Features.GameObjects.Actors.Serialization.Newtonsoft;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Actors.Autofac
{
    public sealed class ActorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<LightRadiusIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InitialStateActorBehaviorsInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<RequiredStatsActorBehaviorsInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorMovementSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorDeathAnimationSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DynamicAnimationBehaviorSerializer>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CorpseInteractionHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}