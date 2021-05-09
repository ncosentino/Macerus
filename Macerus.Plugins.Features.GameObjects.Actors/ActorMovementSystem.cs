using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Api.Behaviors.Filtering;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorMovementSystem : IDiscoverableSystem
    {
        private readonly IBehaviorFinder _behaviorFinder;
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly ICombatTurnManager _combatTurnManager;
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly ILogger _logger;

        public ActorMovementSystem(
            IBehaviorFinder behaviorFinder,
            IActorIdentifiers actorIdentifiers,
            ICombatTurnManager combatTurnManager,
            IFilterContextProvider filterContextProvider,
            ILogger logger)
        {
            _behaviorFinder = behaviorFinder;
            _actorIdentifiers = actorIdentifiers;
            _combatTurnManager = combatTurnManager;
            _filterContextProvider = filterContextProvider;
            _logger = logger;
        }

        public int? Priority => null;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IGameObject> gameObjects)
        {
            var elapsedTime = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value;
            var elapsedSeconds = ((IInterval<double>)elapsedTime.Interval).Value / 1000;

            foreach (var supportedEntry in GetSupportedEntries(gameObjects))
            {
                var movementBehavior = supportedEntry.Item1;
                var dynamicAnimationBehavior = supportedEntry.Item2;
                var worldLocationBehavior = supportedEntry.Item3;

                InhibitNonTurnMovement(movementBehavior);
                WalkPath(
                    movementBehavior,
                    worldLocationBehavior,
                    elapsedSeconds);
                UpdateVelocity(
                    movementBehavior,
                    elapsedSeconds);
                UpdateDirection(
                    movementBehavior);
                UpdateAnimation(
                    movementBehavior,
                    dynamicAnimationBehavior,
                    elapsedSeconds);
            }
        }

        private void InhibitNonTurnMovement(IMovementBehavior movementBehavior)
        {
            if (_combatTurnManager.InCombat &&
                (movementBehavior.ThrottleX != 0 ||
                movementBehavior.ThrottleY != 0))
            {
                var actor = movementBehavior.Owner;
                if (!_combatTurnManager.GetSnapshot(_filterContextProvider.GetContext(), 1).Single().Equals(actor))
                {
                    movementBehavior.SetThrottle(0, 0);
                }
            }
        }

        private IEnumerable<Tuple<IMovementBehavior, IDynamicAnimationBehavior, IWorldLocationBehavior>> GetSupportedEntries(IEnumerable<IGameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                Tuple<IMovementBehavior, IDynamicAnimationBehavior, IWorldLocationBehavior> requiredBehaviors;
                if (!_behaviorFinder.TryFind(
                    gameObject,
                    out requiredBehaviors))
                {
                    continue;
                }

                yield return requiredBehaviors;
            }
        }

        private void UpdateDirection(
            IMovementBehavior movementBehavior)
        {
            var throttleX = movementBehavior.ThrottleX;
            var throttleY = movementBehavior.ThrottleY;

            if (throttleX > 0)
            {
                movementBehavior.SetDirection(2);
            }
            else if (throttleX < 0)
            {
                movementBehavior.SetDirection(0);
            }
            else if (throttleY > 0)
            {
                movementBehavior.SetDirection(1);
            }
            else if (throttleY < 0)
            {
                movementBehavior.SetDirection(3);
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
            else if (animationBehavior.BaseAnimationId?.Equals(_actorIdentifiers.AnimationWalkForward) == true)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationStandForward;
            }
            else if (animationBehavior.BaseAnimationId == null)
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationStandForward;
                _logger.Debug(
                    $"Switching animation to '{animationBehavior.BaseAnimationId}' " +
                    $"on game object '{animationBehavior.Owner}' because the " +
                    $"animation ID was unset.");
            }

            if (lastAnimationId != animationBehavior.BaseAnimationId)
            {
                //_logger.Debug(
                //    $"Switching animation to '{animationBehavior.BaseAnimationId}' " +
                //    $"on game object '{animationBehavior.Owner}'. Current " +
                //    $"animation is '{animationBehavior.CurrentAnimationId}'.");
            }
        }

        private void UpdateVelocity(
            IMovementBehavior movementBehavior,
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

            // walking on diagonals shouldn't make you go super fast
            // FIXME: only care about this at max speed because click-to-move
            // acceleration is so slow this becomes painful to watch
            if (Math.Abs(velocityX) == MAX_VELOCITY_RANGE_ABS &&
                Math.Abs(velocityY) == MAX_VELOCITY_RANGE_ABS)
            {
                velocityX /= 1.41d;
                velocityY /= 1.41d;
            }

            movementBehavior.SetVelocity(
                velocityX,
                velocityY);
        }

        private void WalkPath(
            IMovementBehavior movementBehavior,
            IWorldLocationBehavior locationBehavior,
            double elapsedSeconds)
        {
            if (movementBehavior.PointsToWalk.Count < 1)
            {
                return;
            }

            var currentWalkPoint = movementBehavior.PointsToWalk.First();

            // try to stay a little further on the last point so we don't slam into the target
            double closeEnough = movementBehavior.PointsToWalk.Count == 1
                ? 0.50
                : 0.25;
            if (Math.Abs(locationBehavior.X - currentWalkPoint.X) +
                Math.Abs(locationBehavior.Y - currentWalkPoint.Y) < closeEnough)
            {
                movementBehavior.CompleteWalkPoint();

                if (movementBehavior.PointsToWalk.Count < 1)
                {
                    _logger.Debug(
                        $"'{movementBehavior.Owner}' walked to their target at " +
                        $"'({currentWalkPoint.X},{currentWalkPoint.Y})'.");
                    movementBehavior.SetThrottle(0, 0);
                    return;
                }
                else
                {
                    var nextWalkPoint = movementBehavior.PointsToWalk.First();
                    _logger.Debug(
                        $"'{movementBehavior.Owner}' walked to '({currentWalkPoint.X},{currentWalkPoint.Y})' " +
                        $"and will walk to ({nextWalkPoint.X},{nextWalkPoint.Y}) next.");
                    currentWalkPoint = nextWalkPoint;
                }
            }

            const double DEBOUNCE = 0.1;
            const double DAMPENING_FACTOR = 0.1;
            var previousThrottleX = movementBehavior.ThrottleX;
            var previousThrottleY = movementBehavior.ThrottleY;
            var throttleX = 0d;
            var throttleY = 0d;
            if (Math.Abs(locationBehavior.X - currentWalkPoint.X) < DEBOUNCE)
            {
                throttleX = 0;
            }
            else if (locationBehavior.X < currentWalkPoint.X)
            {
                throttleX = throttleX < 0 ? 0 : previousThrottleX + DAMPENING_FACTOR * elapsedSeconds;
            }
            else if (locationBehavior.X > currentWalkPoint.X)
            {
                throttleX = throttleX > 0 ? 0 : previousThrottleX - DAMPENING_FACTOR * elapsedSeconds;
            }

            if (Math.Abs(locationBehavior.Y - currentWalkPoint.Y) < DEBOUNCE)
            {
                throttleY = 0;
            }
            else if (locationBehavior.Y < currentWalkPoint.Y)
            {
                throttleY = throttleY < 0 ? 0 : previousThrottleY + DAMPENING_FACTOR * elapsedSeconds;
            }
            else if (locationBehavior.Y > currentWalkPoint.Y)
            {
                throttleY = throttleY > 0 ? 0 : previousThrottleY - DAMPENING_FACTOR * elapsedSeconds;
            }

            throttleX = throttleX > 1 ? 1 : throttleX < -1 ? -1 : throttleX;
            throttleY = throttleY > 1 ? 1 : throttleY < -1 ? -1 : throttleY;
            movementBehavior.SetThrottle(throttleX, throttleY);
        }
    }
}
