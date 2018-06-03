using System;
using Macerus.Api.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public class MovementBehavior :
        BaseBehavior,
        IMovementBehavior
    {
        private const double MAX_THROTTLE_RANGE = 1;
        private const double MIN_THROTTLE_RANGE = -1;
        private const double MIN_THROTTLE_ABS_VALUE = double.Epsilon;

        private double _throttleX;
        private double _throttleY;

        public double ThrottleX
        {
            get { return _throttleX; }
            set
            {
                _throttleX = Math.Abs(value) < MIN_THROTTLE_ABS_VALUE
                    ? 0
                    : Math.Max(MIN_THROTTLE_RANGE, Math.Min(MAX_THROTTLE_RANGE, value));
            }
        }

        public double ThrottleY
        {
            get { return _throttleY; }
            set
            {
                _throttleY = Math.Abs(value) < MIN_THROTTLE_ABS_VALUE
                    ? 0
                    : Math.Max(MIN_THROTTLE_RANGE, Math.Min(MAX_THROTTLE_RANGE, value));
            }
        }

        public double RateOfDeceleration { get; set; } = 1.5;
    }
}