using System;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Framework;

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

            if (!skill.TryGetFirst<IActorAnimationOnUseBehavior>(out var actorAnimationOnUseBehavior))
            {
                return;
            }

            IIdentifier directionReplacementId;
            if (movementBehavior.Direction == 0)
            {
                directionReplacementId = _actorIdentifiers.AnimationDirectionLeft;
            }
            else if (movementBehavior.Direction == 1)
            {
                directionReplacementId = _actorIdentifiers.AnimationDirectionBack;
            }
            else if (movementBehavior.Direction == 2)
            {
                directionReplacementId = _actorIdentifiers.AnimationDirectionRight;
            }
            else if (movementBehavior.Direction == 3)
            {
                directionReplacementId = _actorIdentifiers.AnimationDirectionForward;
            }
            else
            {
                throw new NotSupportedException(
                    $"Could not set an animation for skill usage because the " +
                    $"direction of '{user}' based on their movement behavior " +
                    $"'{movementBehavior}' was {movementBehavior.Direction}.");
            }

            var directionModifiedAnimationId = new StringIdentifier(
                actorAnimationOnUseBehavior
                .AnimationId
                .ToString()
                .Replace(_actorIdentifiers.AnimationDirectionPlaceholder.ToString(), directionReplacementId.ToString()));
            animationBehavior.BaseAnimationId = directionModifiedAnimationId;
        }
    }
}
