using ProjectXyz.Api.Behaviors;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlyWorldLocationBehavior : IBehavior
    {
        double X { get; }

        double Y { get; }
    }
}