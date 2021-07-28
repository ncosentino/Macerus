using Macerus.Plugins.Features.Interactions.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Interactions
{
    public interface IActorInteractablesBehavior : IReadOnlyActorInteractablesBehavior
    {
        void AddInteractable(IInteractableBehavior interactable);

        void RemoveInteractable(IInteractableBehavior interactable);
    }
}
