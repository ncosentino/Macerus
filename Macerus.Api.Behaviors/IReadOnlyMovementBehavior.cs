using System;
using System.Collections.Generic;
using System.Numerics;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlyMovementBehavior : IBehavior
    {
        double ThrottleX { get; }

        double ThrottleY { get; }

        double VelocityX { get; }

        double VelocityY { get; }

        int Direction { get; }

        IReadOnlyCollection<Vector2> PointsToWalk { get; }

        Vector2? CurrentWalkTarget { get; }

        Vector2? CurrentWalkSource { get; }

        double CurrentWalkSegmentDistance { get; }

        TimeSpan CurrentWalkSegmentElapsedTime { get; }
    }
}