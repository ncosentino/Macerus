using System.Collections.Generic;
using System.Numerics;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Summoning
{
    public interface ISummonTargetLocationBehavior : IBehavior
    {
        IReadOnlyCollection<Vector2> Locations { get; }
    }
}
