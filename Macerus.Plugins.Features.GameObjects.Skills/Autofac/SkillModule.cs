using Autofac;

using Macerus.Plugins.Features.GameObjects.Skills.Default;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Skills.Autofac
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
        }
    }
}
