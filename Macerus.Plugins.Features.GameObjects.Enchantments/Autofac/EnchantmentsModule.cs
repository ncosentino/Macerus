using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Autofac
{
    public sealed class EnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<EnchantmentTargetFilterComponentConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasStatFilterComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<RandomRangeExpressionFilterComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .RegisterType<EnchantmentsGenerator>()
                   .AsImplementedInterfaces()
                   .SingleInstance();
        }
    }
}
