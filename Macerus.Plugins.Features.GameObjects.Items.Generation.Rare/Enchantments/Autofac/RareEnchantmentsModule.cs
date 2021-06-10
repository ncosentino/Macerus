using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare.Enchantments.Autofac
{
    public sealed class RareEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<RareEnchantmentGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
