using Autofac;

using Macerus.Plugins.Features.Summoning.Default;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Summoning
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
        }
    }
}
