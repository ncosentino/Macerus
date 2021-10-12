using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Content.Enchantments
{
    public sealed class EnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder.RegisterType<EnchantmentDefinitionBuilder>();
            builder.RegisterType<EnchantmentTemplate>().SingleInstance();
        }
    }
}
