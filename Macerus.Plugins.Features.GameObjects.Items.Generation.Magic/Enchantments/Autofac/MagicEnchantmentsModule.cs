using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic
{
    public sealed class MagicEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MagicEnchantmentGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();      
        }
    }
}
