using System;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
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
            var doorInteractableBehavior = (DoorInteractableBehavior)behavior;

            if (doorInteractableBehavior.TransitionToMapId != null)
            {
                _mapManager.SwitchMap(doorInteractableBehavior.TransitionToMapId);
            }

            if (doorInteractableBehavior.TransitionToX != null ||
                doorInteractableBehavior.TransitionToY != null)
            {
                var actorPositionBehavior = actor.GetOnly<IPositionBehavior>();
                actorPositionBehavior.SetPosition(
                    doorInteractableBehavior.TransitionToX != null
                        ? doorInteractableBehavior.TransitionToX.Value
                        : actorPositionBehavior.X,
                    doorInteractableBehavior.TransitionToY != null
                        ? doorInteractableBehavior.TransitionToY.Value
                        : actorPositionBehavior.Y);
            }
        }
    }
}
