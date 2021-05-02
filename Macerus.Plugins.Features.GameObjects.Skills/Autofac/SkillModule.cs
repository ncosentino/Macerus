using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default.Autofac
{
    public sealed class SkillModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<SkillIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
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
                .RegisterType<EnchantTargetsSkillHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AnimateSkillUserSkillHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InflictDamageSkillHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillTargetingAmenity>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
