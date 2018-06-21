using Macerus.Api.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public class MovementBehavior :
        BaseBehavior,
        IMovementBehavior
    {
        public double ThrottleX { get; set; }

        public double ThrottleY { get; set; }

        public double RateOfDeceleration { get; set; } = 1.5;
    }
}