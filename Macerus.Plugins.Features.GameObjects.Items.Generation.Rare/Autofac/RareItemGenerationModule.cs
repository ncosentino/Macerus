using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public sealed class RareItemGenerationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<RareItemGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<RareItemNameGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<RareItemNameGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<RareAffixRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
