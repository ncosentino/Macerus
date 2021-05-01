using System;

using Macerus.Api.Behaviors;
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

            if (doorInteractableBehavior.TransitionToX != null ||
                doorInteractableBehavior.TransitionToY != null)
            {
                var actorLocationBehavior = actor.GetOnly<IWorldLocationBehavior>();
                actorLocationBehavior.SetLocation(
                    doorInteractableBehavior.TransitionToX != null
                        ? doorInteractableBehavior.TransitionToX.Value
                        : actorLocationBehavior.X,
                    doorInteractableBehavior.TransitionToY != null
                        ? doorInteractableBehavior.TransitionToY.Value
                        : actorLocationBehavior.Y);
            }
        }
    }
}
