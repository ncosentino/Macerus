using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

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
            builder
               .RegisterType<RareItemTagsFilterHandler>()
               .SingleInstance();
            builder
                .RegisterBuildCallback(c =>
                {
                    var facade = c.Resolve<IAttributeValueMatchFacade>();
                    facade.Register(c.Resolve<RareItemTagsFilterHandler>().Matcher);
                });
        }
    }
}
