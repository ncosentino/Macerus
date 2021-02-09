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
            const double SPEED = 2.5f;
            const double MAX_THROTTLE_RANGE = 1;
            const double MIN_THROTTLE_RANGE = -1;
            const double MIN_THROTTLE_ABS_VALUE = 0.01;

            // TODO: the rate of decel could be affected by the type of tile you're on?
            // ice might have a way lower rate, etc...
            var timeAdjustedDecelerate = elapsedSeconds * movementBehavior.RateOfDeceleration;

            var throttleX = movementBehavior.ThrottleX;
            var throttleY = movementBehavior.ThrottleY;

            if (throttleX >= MIN_THROTTLE_ABS_VALUE)
            {
                throttleX = throttleX - timeAdjustedDecelerate;
            }
            else if (throttleX < -1 * MIN_THROTTLE_ABS_VALUE)
            {
                throttleX = throttleX + timeAdjustedDecelerate;
            }
            else
            {
                throttleX = 0;
            }

            throttleX = Math.Abs(throttleX) <= MIN_THROTTLE_ABS_VALUE
                ? 0
                : Math.Max(MIN_THROTTLE_RANGE, Math.Min(MAX_THROTTLE_RANGE, throttleX));

            if (throttleY >= MIN_THROTTLE_ABS_VALUE)
            {
                throttleY = throttleY - timeAdjustedDecelerate;
            }
            else if (throttleY < -1 * MIN_THROTTLE_ABS_VALUE)
            {
                throttleY = throttleY + timeAdjustedDecelerate;
            }
            else
            {
                throttleY = 0;
            }

            throttleY = Math.Abs(throttleY) <= MIN_THROTTLE_ABS_VALUE
                ? 0
                : Math.Max(MIN_THROTTLE_RANGE, Math.Min(MAX_THROTTLE_RANGE, throttleY));

            movementBehavior.ThrottleX = throttleX;
            movementBehavior.ThrottleY = throttleY;

            var xAdjust = SPEED * elapsedSeconds * throttleX;
            var yAdjust = SPEED * elapsedSeconds * throttleY;

            if (Math.Abs(xAdjust) > double.Epsilon)
            {
                worldLocationBehavior.X += xAdjust;
            }

            if (Math.Abs(yAdjust) > double.Epsilon)
            {
                worldLocationBehavior.Y += yAdjust;
            }
        }
    }
}
