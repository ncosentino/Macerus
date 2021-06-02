using System;
using System.Collections.Generic;
using System.Numerics;

namespace Macerus.Api.Behaviors
{
    public interface IMovementBehavior : IObservableMovementBehavior
    {
        new TimeSpan CurrentWalkSegmentElapsedTime { get; set; }

        void SetThrottle(double throttleX, double throttleY);

        void SetVelocity(double velocityX, double velocityY);

        void SetDirection(int direction);

        Tuple<Vector2?, Vector2?> StartNextWalkPoint();

        void SetWalkPath(IEnumerable<Vector2> pointsToWalk);
    }
}