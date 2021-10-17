using System;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Default;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Affixes
{
    public sealed class MagicAffixesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var affixTemplate = c.Resolve<AffixTemplate>();
                    var affixDefinitions = new[]
                    {
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("lively"),
                            0,
                            10,
                            new IntIdentifier(1),
                            new IntIdentifier(2),
                            new StringIdentifier("lively-ench")),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("hearty"),
                            5,
                            20,
                            new IntIdentifier(3),
                            new IntIdentifier(4),
                            new StringIdentifier("hearty-ench")),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic"),
                            0,
                            10,
                            new IntIdentifier(5),
                            new IntIdentifier(6),
                            new StringIdentifier("magic-ench")),
                    };
                    var repository = new InMemoryAffixDefinitionRepository(
                        c.Resolve<Lazy<IAttributeFilterer>>(),
                        affixDefinitions);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();    
        }
    }
}
