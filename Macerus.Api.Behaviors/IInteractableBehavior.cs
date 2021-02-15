using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Api.Behaviors
{
    public interface IInteractableBehavior : IBehavior
    {
        bool AutomaticInteraction { get; }

        void Interact(IGameObject actor);
    }
}