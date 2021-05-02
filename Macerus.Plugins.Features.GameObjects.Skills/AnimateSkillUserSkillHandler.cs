using System;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class AnimateSkillUserSkillHandler : IDiscoverableSkillHandler
    {
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly ILogger _logger;

        public AnimateSkillUserSkillHandler(
            IActorIdentifiers actorIdentifiers,
            ILogger logger)
        {
            _actorIdentifiers = actorIdentifiers;
            _logger = logger;
        }

        public int? Priority { get; } = int.MinValue;

        public void Handle(
            IGameObject user,
            IGameObject skill)
        {
            if (!user.TryGetFirst<IDynamicAnimationBehavior>(out var animationBehavior))
            {
                return;
            }

            if (!user.TryGetFirst<IReadOnlyMovementBehavior>(out var movementBehavior))
            {
                return;
            }

            // FIXME: check the behavior on the skill to see if we have an
            // attack with a weapon or a casting action

            if (movementBehavior.Direction == 0)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationCastLeft;
            }
            else if (movementBehavior.Direction == 1)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationCastBack;
            }
            else if (movementBehavior.Direction == 2)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationCastRight;
            }
            else if (movementBehavior.Direction == 3)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationCastForward;
            }
            else
            {
                throw new NotSupportedException(
                    $"Could not set an animation for skill usage because the " +
                    $"direction of '{user}' based on their movement behavior " +
                    $"'{movementBehavior}' was {movementBehavior.Direction}.");
            }
        }
    }
}
