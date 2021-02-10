using System;
using System.Collections.Generic;
using Macerus.Api.Behaviors;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorMovementSystem : ISystem
    {
        private static readonly IIdentifier STAND_BACK_ANIMATION = new StringIdentifier("player_stand_back");
        private static readonly IIdentifier STAND_FORWARD_ANIMATION = new StringIdentifier("player_stand_forward");
        private static readonly IIdentifier STAND_LEFT_ANIMATION = new StringIdentifier("player_stand_left");
        private static readonly IIdentifier STAND_RIGHT_ANIMATION = new StringIdentifier("player_stand_right");
        private static readonly IIdentifier WALK_BACK_ANIMATION = new StringIdentifier("player_walk_back");
        private static readonly IIdentifier WALK_FORWARD_ANIMATION = new StringIdentifier("player_walk_forward");
        private static readonly IIdentifier WALK_LEFT_ANIMATION = new StringIdentifier("player_walk_left");
        private static readonly IIdentifier WALK_RIGHT_ANIMATION = new StringIdentifier("player_walk_right");

        private readonly IBehaviorFinder _behaviorFinder;
        private readonly ILogger _logger;

        public ActorMovementSystem(
            IBehaviorFinder behaviorFinder,
            ILogger logger)
        {
            _behaviorFinder = behaviorFinder;
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

        private IEnumerable<Tuple<IWorldLocationBehavior, IMovementBehavior, IAnimationBehavior>> GetSupportedEntries(IEnumerable<IHasBehaviors> hasBehaviors)
        {
            foreach (var gameObject in hasBehaviors)
            {
                Tuple<IWorldLocationBehavior, IMovementBehavior, IAnimationBehavior> requiredBehaviors;
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
            IAnimationBehavior animationBehavior,
            double elapsedSeconds)
        {
            // we use throttle to control the animation because it gives 
            // superior feedback to the user that you're trying to move in a 
            // particular direction over the direction you may actually have 
            // velocity in
            var throttleX = movementBehavior.ThrottleX;
            var throttleY = movementBehavior.ThrottleY;
            var lastAnimationId = animationBehavior.CurrentAnimationId;

            if (throttleX > 0)
            {
                animationBehavior.CurrentAnimationId = WALK_RIGHT_ANIMATION;
            }
            else if (throttleX < 0)
            {
                animationBehavior.CurrentAnimationId = WALK_LEFT_ANIMATION;
            }
            else if (throttleY > 0)
            {
                animationBehavior.CurrentAnimationId = WALK_BACK_ANIMATION;
            }
            else if (throttleY < 0)
            {
                animationBehavior.CurrentAnimationId = WALK_FORWARD_ANIMATION;
            }
            else if (animationBehavior.CurrentAnimationId?.Equals(WALK_RIGHT_ANIMATION) == true)
            {
                animationBehavior.CurrentAnimationId = STAND_RIGHT_ANIMATION;
            }
            else if (animationBehavior.CurrentAnimationId?.Equals(WALK_LEFT_ANIMATION) == true)
            {
                animationBehavior.CurrentAnimationId = STAND_LEFT_ANIMATION;
            }
            else if (animationBehavior.CurrentAnimationId?.Equals(WALK_BACK_ANIMATION) == true)
            {
                animationBehavior.CurrentAnimationId = STAND_BACK_ANIMATION;
            }
            else if (animationBehavior.CurrentAnimationId?.Equals(STAND_BACK_ANIMATION) != true &&
                animationBehavior.CurrentAnimationId?.Equals(STAND_FORWARD_ANIMATION) != true &&
                animationBehavior.CurrentAnimationId?.Equals(STAND_LEFT_ANIMATION) != true &&
                animationBehavior.CurrentAnimationId?.Equals(STAND_RIGHT_ANIMATION) != true)
            {
                animationBehavior.CurrentAnimationId = STAND_FORWARD_ANIMATION;
            }

            if (lastAnimationId != animationBehavior.CurrentAnimationId)
            {
                _logger.Debug(
                    $"Switching animation to '{animationBehavior.CurrentAnimationId}' " +
                    $"on game object '{animationBehavior.Owner}' with animation " +
                    $"behavior.");
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
