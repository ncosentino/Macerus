﻿using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors.Interactions;
using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Interactions
{
    public sealed class ActorInteractionManager : IActorInteractionManager
    {
        private readonly IInteractionHandlerFacade _interactionHandler;
        private readonly IActorActionCheck _actorActionCheck;

        public ActorInteractionManager(
            IInteractionHandlerFacade interactionHandler,
            IActorActionCheck actorActionCheck)
        {
            _interactionHandler = interactionHandler;
            _actorActionCheck = actorActionCheck;
        }

        public async Task<bool> TryInteractAsync(
            IFilterContext filterContext,
            IGameObject actor)
        {
            // stop moving if we try to interact
            var movementBehavior = actor.GetOnly<IMovementBehavior>();
            movementBehavior.SetVelocity(0, 0);
            movementBehavior.SetThrottle(0, 0);
            movementBehavior.ClearWalkPath();

            if (!_actorActionCheck.CanAct(
                filterContext,
                actor))
            {
                return false;
            }

            var actorInteractablesBehavior = actor.GetOnly<IReadOnlyActorInteractablesBehavior>();
            var interactable = actorInteractablesBehavior
                .Interactables
                .FirstOrDefault();
            if (interactable == null)
            {
                return false;
            }

            await _interactionHandler
                .InteractAsync(filterContext, actor, interactable)
                .ConfigureAwait(false);
            return true;
        }

        public async Task ObjectEnterInteractionRadiusAsync(
            IFilterContext filterContext,
            IGameObject actor,
            IGameObject gameObject)
        {
            if (!gameObject.TryGetFirst<IInteractableBehavior>(out var interactionBehavior))
            {
                return;
            }

            if (interactionBehavior.AutomaticInteraction)
            {
                await _interactionHandler
                    .InteractAsync(filterContext, actor, interactionBehavior)
                    .ConfigureAwait(false);
                return;
            }

            var actorInteractablesBehavior = actor.GetOnly<IActorInteractablesBehavior>();
            actorInteractablesBehavior.AddInteractable(interactionBehavior);

            // FIXME: this is where the noise-playing hook was
        }

        public async Task ObjectExitInteractionRadiusAsync(
            IFilterContext filterContext,
            IGameObject actor,
            IGameObject gameObject)
        {
            if (!gameObject.TryGetFirst<IInteractableBehavior>(out var interactionBehavior))
            {
                return;
            }

            var actorInteractablesBehavior = actor.GetOnly<IActorInteractablesBehavior>();
            actorInteractablesBehavior.RemoveInteractable(interactionBehavior);
        }
    }
}
