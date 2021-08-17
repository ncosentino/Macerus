using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummonTargetLocationBehavior :
        BaseBehavior,
        ISummonTargetLocationBehavior
    {
        public SummonTargetLocationBehavior(IEnumerable<Vector2> locations)
        {
            Locations = locations.ToArray();
        }

        public IReadOnlyCollection<Vector2> Locations { get; }
    }
}
