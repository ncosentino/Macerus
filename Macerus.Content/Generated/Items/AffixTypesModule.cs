using System;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Default;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Items
{
    public sealed class AffixTypesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var affixTypes = new[]
                    {
                          new AffixType(new StringIdentifier("affix_type_1"), "normal"),
                          new AffixType(new StringIdentifier("affix_type_2"), "magic"),
                          new AffixType(new StringIdentifier("affix_type_3"), "rare"),
                          new AffixType(new StringIdentifier("affix_type_4"), "imbued"),
                          new AffixType(new StringIdentifier("affix_type_5"), "unique"),
                          new AffixType(new StringIdentifier("affix_type_6"), "legendary"),
                          new AffixType(new StringIdentifier("affix_type_7"), "relic"),
                          new AffixType(new StringIdentifier("affix_type_8"), "mythic")
                    };
                    var repository = new InMemoryAffixTypeRepository(affixTypes);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }
}