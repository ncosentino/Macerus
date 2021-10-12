using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Default.MySql;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default.Autofac
{
    public sealed class AffixesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<AffixTypeRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AffixDefinitionRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<RandomAffixGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AffixEnchantmentsGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}