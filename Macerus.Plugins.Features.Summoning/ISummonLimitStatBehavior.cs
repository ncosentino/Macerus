using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Summoning
{
    public interface ISummonLimitStatBehavior : IBehavior
    {
        IIdentifier SummonLimitStatPairId { get; }
    }
}
