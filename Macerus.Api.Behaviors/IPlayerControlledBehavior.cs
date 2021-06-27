using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Api.Behaviors
{
    public interface IPlayerControlledBehavior : IBehavior
    {
        bool IsActive { get; set; }
    }
}