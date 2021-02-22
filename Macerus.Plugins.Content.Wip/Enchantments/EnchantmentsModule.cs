using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class EnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ValueMapperRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .RegisterType<StateIdToTermRepo>()
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }
}
