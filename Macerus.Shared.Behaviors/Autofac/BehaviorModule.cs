using Autofac;

using Macerus.Shared.Behaviors.Filtering;
using Macerus.Shared.Behaviors.Triggering;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Shared.Behaviors.Autofac
{
    public sealed class BehaviorModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<HasSkillsGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasStatsGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<FilterContextAmenity>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnchantmentOnHitTriggerMechanicFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
