using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default.Autofac
{
    public sealed class SkillModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<SkillAmenity>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<SkillUsage>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
                .RegisterType<SkillHandlerFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnchantTargetsSkillEffectHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AnimateSkillUserSkillEffectHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InflictDamageSkillEffectHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ElapseActionSkillHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillTargetingAmenity>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
