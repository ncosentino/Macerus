using System.Linq;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Api;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default.Autofac
{
    public sealed class AffixesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c => new InMemoryAffixTypeRepository(Enumerable.Empty<IAffixType>()))
                .IfNotRegistered(typeof(IDiscoverableReadOnlyAffixDefinitionRepository))
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
        }
    }
}