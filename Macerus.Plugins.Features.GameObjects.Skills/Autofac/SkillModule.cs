using Autofac;

using Macerus.Plugins.Features.GameObjects.Skills.Default;

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
                .RegisterType<EnchantSelfSkillHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnchantTargetsSkillHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InflictDamageSkillHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillIconGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillDisplayNameGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
