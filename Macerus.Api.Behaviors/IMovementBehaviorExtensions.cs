using System.Numerics;

namespace Macerus.Api.Behaviors
{
    public static class IMovementBehaviorExtensions
    {
        public static Vector2 GetThrottleDirection(this IReadOnlyMovementBehavior movementBehavior)
        {
            var directionVector = Vector2.Normalize(new Vector2(
                (float)movementBehavior.ThrottleX,
                (float)movementBehavior.ThrottleY));
            return directionVector;
        }

        public static bool HasThrottle(this IReadOnlyMovementBehavior movementBehavior)
        {
            var has = movementBehavior.GetThrottleDirection().LengthSquared() > 0;
            return has;
        }

        public static Vector2 GetVelocityDirection(this IReadOnlyMovementBehavior movementBehavior)
        {
            var directionVector = Vector2.Normalize(new Vector2(
                (float)movementBehavior.VelocityX,
                (float)movementBehavior.VelocityY));
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
    }
}