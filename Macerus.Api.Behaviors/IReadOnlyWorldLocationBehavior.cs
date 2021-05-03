using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlyWorldLocationBehavior : IBehavior
    {
        double X { get; }

        double Y { get; }

        double Width { get; }

        double Height { get; }
    }
}