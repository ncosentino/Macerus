using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.AI
{
    public sealed class WalkZoneAITaskBehavior :
        BaseBehavior,
        IWalkZoneAITaskBehavior
    {
        public WalkZoneAITaskBehavior(
            double weight,
            double x,
            double y,
            double width,
            double height)
        {
            Weight = weight;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public double Weight { get; }

        public double X { get; }

        public double Y { get; }

        public double Width { get; }

        public double Height { get; }
    }
}
