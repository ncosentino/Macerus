using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Api.Behaviors
{
    public interface ICircleColliderBehavior : IBehavior
    {
        double X { get; }

        double Y { get; }

        double Radius { get; }

        bool IsTrigger { get; }
    }
}