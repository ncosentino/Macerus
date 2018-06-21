using System;
using System.Collections.Generic;
using Macerus.Api.Behaviors;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Framework.Entities.Extensions;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorMovementSystem : ISystem
    {
        private readonly IBehaviorFinder _behaviorFinder;

        public ActorMovementSystem(IBehaviorFinder behaviorFinder)
        {
            _behaviorFinder = behaviorFinder;
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
            }
        }

        private IEnumerable<Tuple<IWorldLocationBehavior, IMovementBehavior>> GetSupportedEntries(IEnumerable<IHasBehaviors> hasBehaviors)
        {
            foreach (var gameObject in hasBehaviors)
            {
                Tuple<IWorldLocationBehavior, IMovementBehavior> requiredBehaviors;
                if (!_behaviorFinder.TryFind(
                    gameObject,
                    out requiredBehaviors))
                {
                    continue;
                }

                yield return requiredBehaviors;
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
