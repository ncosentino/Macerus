using Autofac;

using Macerus.Plugins.Features.GameObjects.Actors.Animations;
using Macerus.Plugins.Features.GameObjects.Actors.LightRadius;
using Macerus.Plugins.Features.GameObjects.Actors.Npc;
using Macerus.Plugins.Features.GameObjects.Actors.Player;

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
                .RegisterType<DynamicAnimationSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorBehaviorsProvider>()
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
                .RegisterType<ActorTemplateRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PlayerTemplateRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DynamicAnimationIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DynamicAnimationBehaviorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CorpseInteractionHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}