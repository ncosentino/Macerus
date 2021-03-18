using System;
using System.Collections.Generic;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorMovementSystem : ISystem
    {
        private readonly IBehaviorFinder _behaviorFinder;
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly ILogger _logger;

        public ActorMovementSystem(
            IBehaviorFinder behaviorFinder,
            IActorIdentifiers actorIdentifiers,
            ILogger logger)
        {
            _behaviorFinder = behaviorFinder;
            _actorIdentifiers = actorIdentifiers;
            _logger = logger;
        }

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsed = (IInterval<double>)systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;
            var elapsedSeconds = elapsed.Value / 1000;

            foreach (var supportedEntry in GetSupportedEntries(hasBehaviors))
            {
                UpdatePosition(
                    supportedEntry.Item2,
                    supportedEntry.Item1,
                    elapsedSeconds);
                UpdateAnimation(
                    supportedEntry.Item2,
                    supportedEntry.Item3,
                    elapsedSeconds);
            }
        }

        private IEnumerable<Tuple<IWorldLocationBehavior, IMovementBehavior, IDynamicAnimationBehavior>> GetSupportedEntries(IEnumerable<IHasBehaviors> hasBehaviors)
        {
            foreach (var gameObject in hasBehaviors)
            {
                Tuple<IWorldLocationBehavior, IMovementBehavior, IDynamicAnimationBehavior> requiredBehaviors;
                if (!_behaviorFinder.TryFind(
                    gameObject,
                    out requiredBehaviors))
                {
                    continue;
                }

                yield return requiredBehaviors;
            }
        }

        private void UpdateAnimation(
            IMovementBehavior movementBehavior,
            IDynamicAnimationBehavior animationBehavior,
            double elapsedSeconds)
        {
            // we use throttle to control the animation because it gives 
            // superior feedback to the user that you're trying to move in a 
            // particular direction over the direction you may actually have 
            // velocity in
            var throttleX = movementBehavior.ThrottleX;
            var throttleY = movementBehavior.ThrottleY;
            var lastAnimationId = animationBehavior.BaseAnimationId;

            if (throttleX > 0)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationWalkRight;
            }
            else if (throttleX < 0)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationWalkLeft;
            }
            else if (throttleY > 0)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationWalkBack;
            }
            else if (throttleY < 0)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationWalkForward;
            }
            else if (animationBehavior.BaseAnimationId?.Equals(_actorIdentifiers.AnimationWalkRight) == true)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationStandRight;
            }
            else if (animationBehavior.BaseAnimationId?.Equals(_actorIdentifiers.AnimationWalkLeft) == true)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationStandLeft;
            }
            else if (animationBehavior.BaseAnimationId?.Equals(_actorIdentifiers.AnimationWalkBack) == true)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationStandBack;
            }
            else if (animationBehavior.BaseAnimationId?.Equals(_actorIdentifiers.AnimationStandBack) != true &&
                animationBehavior.BaseAnimationId?.Equals(_actorIdentifiers.AnimationStandForward) != true &&
                animationBehavior.BaseAnimationId?.Equals(_actorIdentifiers.AnimationStandLeft) != true &&
                animationBehavior.BaseAnimationId?.Equals(_actorIdentifiers.AnimationStandRight) != true)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationStandForward;
            }

            if (lastAnimationId != animationBehavior.BaseAnimationId)
            {
                //_logger.Debug(
                //    $"Switching animation to '{animationBehavior.BaseAnimationId}' " +
                //    $"on game object '{animationBehavior.Owner}'. Current " +
                //    $"animation is '{animationBehavior.CurrentAnimationId}'.");
            }
        }

        private void UpdatePosition(
            IMovementBehavior movementBehavior,
            IWorldLocationBehavior worldLocationBehavior,
            double elapsedSeconds)
        {
            // TODO: load this from stats?
            const double SPEED = 100f;
            const double MAX_VELOCITY_RANGE_ABS = 2;
            const double MAX_VELOCITY_RANGE = MAX_VELOCITY_RANGE_ABS;
            const double MIN_VELOCITY_RANGE = -1 * MAX_VELOCITY_RANGE_ABS;
            const double MIN_VELOCITY_ABS_VALUE = 0.01;
            const double MIN_THROTTLE_ABS_VALUE = 0.01;
            const double RATE_OF_DECELERATION = 10;

            // TODO: the rate of decel could be affected by the type of tile you're on?
            // ice might have a way lower rate, etc...
            var timeAdjustedDecelerate = elapsedSeconds * RATE_OF_DECELERATION;

            var velocityX = movementBehavior.VelocityX;
            var velocityY = movementBehavior.VelocityY;
            var throttleX = movementBehavior.ThrottleX;
            var throttleY = movementBehavior.ThrottleY;

            var timeAdjustedVelocityAdjustmentX = elapsedSeconds * SPEED * throttleX;

            if (throttleX >= MIN_THROTTLE_ABS_VALUE ||
                throttleX < -1 * MIN_THROTTLE_ABS_VALUE)
            {
                velocityX = velocityX + timeAdjustedVelocityAdjustmentX;
            }
            else if (velocityX >= MIN_VELOCITY_ABS_VALUE)
            {
                velocityX = velocityX - timeAdjustedDecelerate;
            }
            else if (velocityX < -1 * MIN_VELOCITY_ABS_VALUE)
            {
                velocityX = velocityX + timeAdjustedDecelerate;
            }
            else
            {
                velocityX = 0;
            }

            velocityX = Math.Abs(velocityX) <= MIN_VELOCITY_ABS_VALUE
                ? 0
                : Math.Max(MIN_VELOCITY_RANGE, Math.Min(MAX_VELOCITY_RANGE, velocityX));

            var timeAdjustedVelocityAdjustmentY = elapsedSeconds * SPEED * throttleY;

            if (throttleY >= MIN_THROTTLE_ABS_VALUE ||
                throttleY < -1 * MIN_THROTTLE_ABS_VALUE)
            {
                velocityY = velocityY + timeAdjustedVelocityAdjustmentY;
            }
            else if (velocityY >= MIN_VELOCITY_ABS_VALUE)
            {
                velocityY = velocityY - timeAdjustedDecelerate;
            }
            else if (velocityY < -1 * MIN_VELOCITY_ABS_VALUE)
            {
                velocityY = velocityY + timeAdjustedDecelerate;
            }
            else
            {
                velocityY = 0;
            }

            velocityY = Math.Abs(velocityY) <= MIN_VELOCITY_ABS_VALUE
                ? 0
                : Math.Max(MIN_VELOCITY_RANGE, Math.Min(MAX_VELOCITY_RANGE, velocityY));

            // FIXME: walking on diagonals shouldn't make you go super fast
            // TODO: consider if there's risidual velocity or throttle
            if (Math.Abs(velocityX) > double.Epsilon &&
                Math.Abs(velocityY) > double.Epsilon)
            {
            }

            movementBehavior.SetVelocity(
                velocityX,
                velocityY);
        }
    }
}
