using Macerus.Api.Behaviors;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public sealed class CircleColliderBehavior :
        BaseBehavior,
        ICircleColliderBehavior
    {
        public CircleColliderBehavior(
            double x,
            double y,
            double radius,
            bool isTrigger)
        {
            X = x;
            Y = y;
            Radius = radius;
            IsTrigger = isTrigger;
        }

        public double X { get; }

        public double Y { get; }

        public double Radius { get; }

        public bool IsTrigger { get; }
    }
}
