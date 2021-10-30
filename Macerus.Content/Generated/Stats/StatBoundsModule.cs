using System;
using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Stats.Bounded;
using ProjectXyz.Plugins.Features.Stats.Bounded.Default;

using ProjectXyz.Shared.Framework;


namespace Macerus.Content.Generated.Stats
{
    public sealed class StatBoundsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var mapping = new Dictionary<IIdentifier, IStatBounds>()
                    {
                        [new StringIdentifier("stat_1")] = new StatBounds("0", ""),
                        [new StringIdentifier("stat_2")] = new StatBounds("0", "LIFE_MAXIMUM"),
                        [new StringIdentifier("stat_3")] = new StatBounds("0", ""),
                        [new StringIdentifier("stat_4")] = new StatBounds("0", "MANA_MAXIMUM")
                    };
                    var repository = new InMemoryStatDefinitionIdToBoundsMappingRepository(mapping);
                    return repository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
