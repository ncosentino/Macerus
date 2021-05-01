using System.Collections.Generic;
using System.Numerics;

namespace Macerus.Api.Behaviors
{
    public interface IMovementBehavior : IObservableMovementBehavior
    {
        new double ThrottleX { get; set; }

        new double ThrottleY { get; set; }

        void SetThrottle(double throttleX, double throttleY);

        new double VelocityX { get; set; }

        new double VelocityY { get; set; }

        void SetVelocity(double velocityX, double velocityY);

        void SetWalkPath(IEnumerable<Vector2> pointsToWalk);

        Vector2 CompleteWalkPoint();

        new int Direction { get; }

        void SetDirection(int direction);
    }
}