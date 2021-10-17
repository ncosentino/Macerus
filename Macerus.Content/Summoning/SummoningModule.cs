
using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Summoning.Default;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Summoning
{
    public sealed class SummoningModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(x => new InMemorySummonLimitStatPairRepository(new[]
                {
                    new SummonLimitStatPair(
                        new StringIdentifier("test_summon_skeletons_stat_pair"),
                        new StringIdentifier("current_summon_skeletons"),
                        new StringIdentifier("maximum_summon_skeletons"))
                }))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(x => new SummonHandlerLoadOrder(new Dictionary<Type, int>()
                {
                    [typeof(SpawnSummonHandler)] = 10000,
                    [typeof(SpawnLimitSummonHandler)] = 20000,
                    [typeof(TeamAssignmentSummonHandler)] = 30000,
                }))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
