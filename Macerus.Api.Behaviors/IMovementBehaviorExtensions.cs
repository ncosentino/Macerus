using System;
using System.Linq;
using System.Numerics;

namespace Macerus.Api.Behaviors
{
    public static class IMovementBehaviorExtensions
    {
        public static Vector2 GetThrottleDirection(this IReadOnlyMovementBehavior movementBehavior)
        {
            var throttleVector = new Vector2(
                (float)movementBehavior.ThrottleX,
                (float)movementBehavior.ThrottleY);
            var directionVector = throttleVector == Vector2.Zero 
                ? Vector2.Zero
                : Vector2.Normalize(throttleVector);
            return directionVector;
        }

        public static bool HasThrottle(this IReadOnlyMovementBehavior movementBehavior)
        {
            var has = movementBehavior.GetThrottleDirection().LengthSquared() > 0;
            return has;
        }

        public static Vector2 GetVelocityDirection(this IReadOnlyMovementBehavior movementBehavior)
        {
            var velocityVector = new Vector2(
                (float)movementBehavior.VelocityX,
                (float)movementBehavior.VelocityY);
            var directionVector = velocityVector == Vector2.Zero
                 ? Vector2.Zero
                 : Vector2.Normalize(velocityVector);
            return directionVector;
        }

        public static bool HasVelocity(this IReadOnlyMovementBehavior movementBehavior)
        {
            var has = movementBehavior.GetVelocityDirection().LengthSquared() > 0;
            return has;
        }

        public static bool IsMovementIntended(this IReadOnlyMovementBehavior movementBehavior)
        {
            return
                movementBehavior.HasThrottle() ||
                movementBehavior.HasVelocity();
        }

        public static void ClearWalkPath(this IMovementBehavior movementBehavior)
        {
            movementBehavior.SetWalkPath(Enumerable.Empty<Vector2>());
        }

        public static int SetDirectionByVector(
            this IMovementBehavior movementBehavior,
            Vector2 directionalVector) => SetDirectionByVector(
                movementBehavior,
                directionalVector.X,
                directionalVector.Y);

        public static int SetDirectionByVector(
            this IMovementBehavior movementBehavior,
            double x,
            double y)
        {
            if (Math.Abs(x) > Math.Abs(y))
            {
                if (x > 0)
                {
                    movementBehavior.Direction = 2;
                }
                else if (x < 0)
                {
                    movementBehavior.Direction = 0;
                }
            }
            else
            {
                if (y > 0)
                {
                    movementBehavior.Direction = 1;
                }
                else
                {
                    movementBehavior.Direction = 3;
                }
            }

            return movementBehavior.Direction;
        }
    }
}