using System;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Default;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Affixes
{
    public sealed class RareAffixesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var affixTemplate = c.Resolve<AffixTemplate>();
                    var affixDefinitions = new[]
                    {
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare-life-affix"),
                            0,
                            10,
                            new StringIdentifier("rare-life-ench")),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare-mana-affix"),
                            0,
                            10,
                            new StringIdentifier("rare-mana-ench")),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare-fire-res-affix"),
                            0,
                            10,
                            new StringIdentifier("rare-fire-res-ench")),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare-ice-res-affix"),
                            0,
                            10,
                            new StringIdentifier("rare-ice-res-ench")),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare-water-res-affix"),
                            0,
                            10,
                            new StringIdentifier("rare-water-res-ench")),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare-lightning-res-affix"),
                            0,
                            10,
                            new StringIdentifier("rare-lightning-res-ench")),
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
