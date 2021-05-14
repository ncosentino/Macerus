using Macerus.Api.Behaviors;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public sealed class BoxColliderBehavior :
        BaseBehavior,
        IBoxColliderBehavior
    {
        public BoxColliderBehavior(
            double x,
            double y,
            double width,
            double height,
            bool isTrigger)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            IsTrigger = isTrigger;
        }

        public double X { get; }

        public double Y { get; }

        public double Width { get; }

        public double Height { get; }

        public bool IsTrigger { get; }
    }
}
