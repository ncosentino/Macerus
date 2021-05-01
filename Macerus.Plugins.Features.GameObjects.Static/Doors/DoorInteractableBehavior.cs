using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Static.Doors
{
    public sealed class DoorInteractableBehavior :
       BaseBehavior,
       IInteractableBehavior
    {
        public DoorInteractableBehavior(
            bool automaticInteraction,
            IIdentifier transitionToMapId)
        {
            AutomaticInteraction = automaticInteraction;
            TransitionToMapId = transitionToMapId;
        }

        public bool AutomaticInteraction { get; }

        public IIdentifier TransitionToMapId { get; }
    }
}
