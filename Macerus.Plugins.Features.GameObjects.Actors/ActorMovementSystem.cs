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

            // TODO: the rate of decel could be affected by the type of tile you're on?
            // ice might have a way lower rate, etc...
            var timeAdjustedDecelerate = elapsedSeconds * movementBehavior.RateOfDeceleration;

            var throttleX = movementBehavior.ThrottleX;
            var throttleY = movementBehavior.ThrottleY;

            throttleX = throttleX > 0
                ? (throttleX - timeAdjustedDecelerate)
                : (throttleX + timeAdjustedDecelerate);
            throttleY = throttleY > 0
                ? (throttleY - timeAdjustedDecelerate)
                : (throttleY + timeAdjustedDecelerate);

            movementBehavior.ThrottleX = throttleX;
            movementBehavior.ThrottleY = throttleY;

            worldLocationBehavior.X += SPEED * elapsedSeconds * throttleX;
            worldLocationBehavior.Y += SPEED * elapsedSeconds * throttleY;
        }
    }
}
