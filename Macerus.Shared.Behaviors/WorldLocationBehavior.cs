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
        private double _width;
        private double _height;
        private bool _disableEventChange;

        public event EventHandler<EventArgs> WorldLocationChanged;

        public event EventHandler<EventArgs> SizeChanged;

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

        public double Width
        {
            get { return _width; }
            set
            {
                if (!SetWidthIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    SizeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public double Height
        {
            get { return _height; }
            set
            {
                if (!SetHeightIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    SizeChanged?.Invoke(this, EventArgs.Empty);
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

        public void SetSize(double width, double height)
        {
            try
            {
                _disableEventChange = true;
                var changed = SetWidthIfChanged(width);
                changed |= SetHeightIfChanged(height);

                if (changed)
                {
                    SizeChanged?.Invoke(this, EventArgs.Empty);
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

        private bool SetWidthIfChanged(double value)
        {
            if (Math.Abs(_width - value) < double.Epsilon)
            {
                return false;
            }

            _width = value;
            return true;
        }

        private bool SetHeightIfChanged(double value)
        {
            if (Math.Abs(_height - value) < double.Epsilon)
            {
                return false;
            }

            _height = value;
            return true;
        }
    }
}
