using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummonLimitStatBehavior : 
        BaseBehavior,
        ISummonLimitStatBehavior
    {
        public SummonLimitStatBehavior(IIdentifier summonLimitStatPairId)
        {
            SummonLimitStatPairId = summonLimitStatPairId;
        }

        public IIdentifier SummonLimitStatPairId { get; }
    }
}
