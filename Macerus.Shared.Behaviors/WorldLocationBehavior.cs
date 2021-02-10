using System;
using Macerus.Api.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public sealed class WorldLocationBehavior :
        BaseBehavior,
        IWorldLocationBehavior
    {
        private double _x;
        private double _y;
        private bool _disableEventChange;

        public event EventHandler<EventArgs> WorldLocationChanged;

        public double X
        {
            get { return _x; }
            set
            {
                if (!SetXIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    WorldLocationChanged?.Invoke(this, EventArgs.Empty);
                }
            } 
        }

        public double Y
        {
            get { return _y; }
            set
            {
                if (!SetYIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    WorldLocationChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void SetLocation(double x, double y)
        {
            try
            {
                _disableEventChange = true;
                var changed = SetXIfChanged(x);
                changed |= SetYIfChanged(y);

                if (changed)
                {
                    WorldLocationChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            finally
            {
                _disableEventChange = false;
            }
        }

        private bool SetXIfChanged(double value)
        {
            if (Math.Abs(_x - value) < double.Epsilon)
            {
                return false;
            }

            _x = value;
            return true;
        }

        private bool SetYIfChanged(double value)
        {
            if (Math.Abs(_y - value) < double.Epsilon)
            {
                return false;
            }

            _y = value;
            return true;
        }
    }
}
