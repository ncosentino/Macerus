using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Actors.Interactions;
using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Interactions
{
    public sealed class ActorInteractablesBehavior :
        BaseBehavior,
        IActorInteractablesBehavior
    {
        private readonly List<IInteractableBehavior> _interactables;

        public ActorInteractablesBehavior()
        {
            _interactables = new List<IInteractableBehavior>();
        }

        public IReadOnlyCollection<IInteractableBehavior> Interactables => _interactables;

        public void AddInteractable(IInteractableBehavior interactable) =>
            _interactables.Add(interactable);

        public void RemoveInteractable(IInteractableBehavior interactable) =>
            _interactables.Remove(interactable);
    }
}
