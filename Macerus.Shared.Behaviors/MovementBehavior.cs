using System;
using System.Collections.Generic;
using System.Numerics;

using Macerus.Api.Behaviors;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public sealed class MovementBehavior :
        BaseBehavior,
        IMovementBehavior
    {
        private readonly Queue<Vector2> _pointsToWalk;

        private double _throttleX;
        private double _throttleY;
        private double _velocityX;
        private double _velocityY;
        private bool _disableEventChange;

        public MovementBehavior()
        {
            _pointsToWalk = new Queue<Vector2>();
        }

        public event EventHandler<EventArgs> ThrottleChanged;

        public event EventHandler<EventArgs> VelocityChanged;

        public double ThrottleX
        {
            get { return _throttleX; }
            set
            {
                if (!SetThrottleXIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    ThrottleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public double ThrottleY
        {
            get { return _throttleY; }
            set
            {
                if (!SetThrottleYIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    ThrottleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public double VelocityX
        {
            get { return _velocityX; }
            set
            {
                if (!SetVelocityXIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    VelocityChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public double VelocityY
        {
            get { return _velocityY; }
            set
            {
                if (!SetVelocityYIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    VelocityChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public IReadOnlyCollection<Vector2> PointsToWalk => _pointsToWalk;

        public void SetThrottle(double throttleX, double throttleY)
        {
            try
            {
                _disableEventChange = true;
                var changed = SetThrottleXIfChanged(throttleX);
                changed |= SetThrottleYIfChanged(throttleY);

                if (changed)
                {
                    ThrottleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            finally
            {
                _disableEventChange = false;
            }
        }

        public void SetVelocity(double velocityX, double velocityY)
        {
            try
            {
                _disableEventChange = true;
                var changed = SetVelocityXIfChanged(velocityX);
                changed |= SetVelocityYIfChanged(velocityY);

                if (changed)
                {
                    VelocityChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            finally
            {
                _disableEventChange = false;
            }
        }

        private bool SetThrottleXIfChanged(double value)
        {
            if (Math.Abs(_throttleX - value) < double.Epsilon)
            {
                return false;
            }

            _throttleX = value;
            return true;
        }

        private bool SetThrottleYIfChanged(double value)
        {
            if (Math.Abs(_throttleY - value) < double.Epsilon)
            {
                return false;
            }

            _throttleY = value;
            return true;
        }

        private bool SetVelocityXIfChanged(double value)
        {
            if (Math.Abs(_velocityX - value) < double.Epsilon)
            {
                return false;
            }

            _velocityX = value;
            return true;
        }

        private bool SetVelocityYIfChanged(double value)
        {
            if (Math.Abs(_velocityY - value) < double.Epsilon)
            {
                return false;
            }

            _velocityY = value;
            return true;
        }

        public void SetWalkPath(IEnumerable<Vector2> pointsToWalk)
        {
            _pointsToWalk.Clear();
            foreach (var point in pointsToWalk)
            {
                _pointsToWalk.Enqueue(point);
            }    
        }

        public Vector2 CompleteWalkPoint()
        {
            var lastWalkPoint = _pointsToWalk.Dequeue();
            return lastWalkPoint;
        }
    }
}