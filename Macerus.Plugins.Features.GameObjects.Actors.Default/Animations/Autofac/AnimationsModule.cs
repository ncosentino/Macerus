using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Animations.Autofac
{
    public sealed class AnimationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<DynamicAnimationSystem>()
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
                .RegisterType<AnimationIdReplacementFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DirectionalAnimationIdReplacement>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<BaseAnimationIdByStatReplacement>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}