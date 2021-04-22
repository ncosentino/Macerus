using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Interactions.Api
{
    public interface IInteractionHandler
    {
        void Interact(
            IGameObject actor,
            IInteractableBehavior behavior);
    }
}