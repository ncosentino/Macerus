using System;

using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.GameObjects.Static.Doors
{
    public sealed class DoorInteractionHandler : IDiscoverableInteractionHandler
    {
        private readonly IMapManager _mapManager;

        public DoorInteractionHandler(IMapManager mapManager)
        {
            _mapManager = mapManager;
        }

        public Type InteractableType { get; } = typeof(DoorInteractableBehavior);

        public void Interact(
            IGameObject actor,
            IInteractableBehavior behavior)
        {
            // FIXME: whyyyyy this terrible casting
            var interactableObject = (IGameObject)behavior.Owner;
            var doorInteractableBehavior = (DoorInteractableBehavior)behavior;

            if (doorInteractableBehavior.TransitionToMapId != null)
            {
                _mapManager.SwitchMap(doorInteractableBehavior.TransitionToMapId);
            }
        }
    }
}
