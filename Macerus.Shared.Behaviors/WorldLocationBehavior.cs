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

        public event EventHandler<EventArgs> WorldLocationChanged;

        public double X
        {
            get { return _x; }
            set
            {
                if (Math.Abs(_x - value) < double.Epsilon)
                {
                    return;
                }

                _x = value;
                WorldLocationChanged?.Invoke(this, EventArgs.Empty);
            } 
        }

        public double Y
        {
            get { return _y; }
            set
            {
                if (Math.Abs(_y - value) < double.Epsilon)
                {
                    return;
                }

                _y = value;
                WorldLocationChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
