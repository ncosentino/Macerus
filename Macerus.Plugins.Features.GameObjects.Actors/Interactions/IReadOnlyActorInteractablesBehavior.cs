using System.Collections.Generic;

using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors.Interactions
{
    public interface IReadOnlyActorInteractablesBehavior : IBehavior
    {
        IReadOnlyCollection<IInteractableBehavior> Interactables { get; }
    }
}
