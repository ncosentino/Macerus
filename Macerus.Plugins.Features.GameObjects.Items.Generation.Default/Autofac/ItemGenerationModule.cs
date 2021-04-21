using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Default.Autofac
{
    public sealed class ItemGenerationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<LootGeneratorAmenity>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
