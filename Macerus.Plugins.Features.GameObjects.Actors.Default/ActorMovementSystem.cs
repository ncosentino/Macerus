using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default
{
    public sealed class ActorMovementSystem : IDiscoverableSystem
    {
        private readonly IBehaviorFinder _behaviorFinder;
        private readonly IMacerusActorIdentifiers _actorIdentifiers;
        private readonly ICombatTurnManager _combatTurnManager;
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly ILogger _logger;

        private bool _tileRestrictedMovement;

        public ActorMovementSystem(
            IBehaviorFinder behaviorFinder,
            IMacerusActorIdentifiers actorIdentifiers,
            ICombatTurnManager combatTurnManager,
            IFilterContextProvider filterContextProvider,
            ILogger logger)
        {
            _behaviorFinder = behaviorFinder;
            _actorIdentifiers = actorIdentifiers;
            _combatTurnManager = combatTurnManager;
            _filterContextProvider = filterContextProvider;
            _logger = logger;

            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            _combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;
            var elapsedTime = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value;
            var elapsedSeconds = ((IInterval<double>)elapsedTime.Interval).Value / 1000;

            foreach (var supportedEntry in GetSupportedEntries(turnInfo.AllGameObjects))
            {
                var movementBehavior = supportedEntry.Item1;
                var dynamicAnimationBehavior = supportedEntry.Item2;
                var positionBehavior = supportedEntry.Item3;

                InhibitNonTurnMovement(movementBehavior);

                if (_tileRestrictedMovement)
                {
                    // disallow any potential velocity shinanigans when we're
                    // in tile-restricted mode
                    movementBehavior.SetVelocity(0, 0);

                    // we only have walk-paths in tile-restricted movement.
                    // throttle can be used instead to change direction but
                    // that's it!
                    if (movementBehavior.CurrentWalkTarget.HasValue)
                    {
                        ProcessTileBasedWalkPath(
                            movementBehavior,
                            positionBehavior,
                            elapsedSeconds);
                    }
                }
                else
                {
                    // process throttle movement first to allow path interruption
                    ProcessFreeformThrottleMovement(
                        movementBehavior,
                        positionBehavior,
                        elapsedSeconds);

                    if (movementBehavior.CurrentWalkTarget.HasValue)
                    {
                        ProcessFreeformWalkPath(
                            movementBehavior,
                            positionBehavior,
                            elapsedSeconds);
                    }
                }

                UpdateDirection(movementBehavior);

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

        private IEnumerable<Tuple<IMovementBehavior, IDynamicAnimationBehavior, IPositionBehavior>> GetSupportedEntries(IEnumerable<IGameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                Tuple<IMovementBehavior, IDynamicAnimationBehavior, IPositionBehavior> requiredBehaviors;
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
            // we prioritize throttle to control the direction because it gives 
            // superior feedback to the user that you're trying to move in a 
            // particular direction over the direction you may actually have 
            // velocity in. if there's no throttle we can rely on velocity.
            if (movementBehavior.HasThrottle())
            {
                var throttleX = movementBehavior.ThrottleX;
                var throttleY = movementBehavior.ThrottleY;
                movementBehavior.SetDirectionByVector(throttleX, throttleY);
            }
            else if (movementBehavior.HasVelocity())
            {
                var velocityX = movementBehavior.VelocityX;
                var velocityY = movementBehavior.VelocityY;
                movementBehavior.SetDirectionByVector(velocityX, velocityY);
            }
        }

        private void UpdateAnimation(
            IMovementBehavior movementBehavior,
            IDynamicAnimationBehavior animationBehavior,
            double elapsedSeconds)
        {
            if (movementBehavior.IsMovementIntended())
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationWalk;
            }
            // we want to ensure there isn't some other animation
            // playing, so really we only want to assign a new animation
            // here if we WERE moving (i.e. animation is walk) since now
            // we aren't anymore... but we don't want to cancel things
            // like casting or attacking animations
            else if (animationBehavior.BaseAnimationId == null ||
                Equals(animationBehavior.BaseAnimationId, _actorIdentifiers.AnimationWalk))
            {
                animationBehavior.BaseAnimationId = _actorIdentifiers.AnimationStand;
            }
        }

        private void ProcessFreeformThrottleMovement(
            IMovementBehavior movementBehavior,
            IPositionBehavior positionBehavior,
            double elapsedSeconds)
        {
            if (movementBehavior.HasThrottle())
            {
                movementBehavior.SetWalkPath(Enumerable.Empty<Vector2>());
            }

            if (!movementBehavior.CurrentWalkTarget.HasValue)
            {
                var velocity = VelocityFromThrottle(movementBehavior);
                movementBehavior.SetVelocity(velocity.X, velocity.Y);
            }
        }

        private void ProcessTileBasedWalkPath(
            IMovementBehavior movementBehavior,
            IPositionBehavior positionBehavior,
            double elapsedSeconds)
        {
            if (!TryGetWalkPoint(
                positionBehavior,
                movementBehavior,
                double.Epsilon,
                out var currentWalkPoint))
            {
                return;
            }

            movementBehavior.CurrentWalkSegmentElapsedTime += TimeSpan.FromSeconds(elapsedSeconds);
            var distanceToTravel = GetMaxVelocityAbs() * (float)movementBehavior.CurrentWalkSegmentElapsedTime.TotalSeconds;

            var lerpPercent = Math.Max(0, Math.Min(1, (float)(distanceToTravel / movementBehavior.CurrentWalkSegmentDistance)));
            var nextPosition = Lerp(
                movementBehavior.CurrentWalkSource.Value,
                currentWalkPoint.Value,
                lerpPercent);

            var throttle = Vector2.Normalize(nextPosition - new Vector2((float)positionBehavior.X, (float)positionBehavior.Y));
            movementBehavior.SetThrottle(
                throttle.X > 0 ? 1 : throttle.X < 0 ? -1 : 0,
                throttle.Y > 0 ? 1 : throttle.Y < 0 ? -1 : 0);
            positionBehavior.SetPosition(nextPosition.X, nextPosition.Y);
        }

        private void ProcessFreeformWalkPath(
            IMovementBehavior movementBehavior,
            IPositionBehavior positionBehavior,
            double elapsedSeconds)
        {
            if (!TryGetWalkPoint(
                positionBehavior,
                movementBehavior,
                0.05,
                out var currentWalkPoint))
            {
                return;
            }

            movementBehavior.CurrentWalkSegmentElapsedTime += TimeSpan.FromSeconds(elapsedSeconds);
            var speed = GetMaxVelocityAbs();
            var distanceToTravel = speed * (float)movementBehavior.CurrentWalkSegmentElapsedTime.TotalSeconds;

            var lerpPercent = Math.Max(0, Math.Min(1, (float)(distanceToTravel / movementBehavior.CurrentWalkSegmentDistance)));
            var nextPosition = Lerp(
                movementBehavior.CurrentWalkSource.Value,
                currentWalkPoint.Value,
                lerpPercent);

            var directionUnitVector = nextPosition - new Vector2((float)positionBehavior.X, (float)positionBehavior.Y);
            directionUnitVector = directionUnitVector == Vector2.Zero ? Vector2.Zero : Vector2.Normalize(directionUnitVector);

            var velocity = speed * directionUnitVector;
            movementBehavior.SetVelocity(velocity.X, velocity.Y);
        }

        private bool TryGetWalkPoint(
            IPositionBehavior positionBehavior,
            IMovementBehavior movementBehavior,
            double closeEnough,
            out Vector2? walkPoint)
        {
            walkPoint = movementBehavior.CurrentWalkTarget.Value;
            if (!walkPoint.HasValue)
            {
                return false;
            }

            if (Math.Abs(positionBehavior.X - walkPoint.Value.X) +
                Math.Abs(positionBehavior.Y - walkPoint.Value.Y) < closeEnough)
            {
                var nextWalkInfo = movementBehavior.StartNextWalkPoint();

                if (!nextWalkInfo.Item2.HasValue)
                {
                    _logger.Debug(
                        $"'{movementBehavior.Owner}' walked to their target at " +
                        $"'({walkPoint.Value.X},{walkPoint.Value.Y})'.");
                    positionBehavior.SetPosition(
                        walkPoint.Value.X,
                        walkPoint.Value.Y);
                    return false;
                }
                else
                {
                    var nextWalkPoint = nextWalkInfo.Item2.Value;
                    _logger.Debug(
                        $"'{movementBehavior.Owner}' walked to '({walkPoint.Value.X},{walkPoint.Value.Y})' " +
                        $"and will walk to ({nextWalkPoint.X},{nextWalkPoint.Y}) next.");
                    walkPoint = nextWalkPoint;
                }
            }

            return true;
        }

        private Vector2 VelocityFromThrottle(IMovementBehavior movementBehavior)
        {
            // TODO: load this from stats?
            const double SPEED = 100f;
            const double MIN_VELOCITY_ABS_VALUE = 0.01;
            const double MIN_THROTTLE_ABS_VALUE = 0.01;
            // TODO: the rate of decel could be affected by the type of tile you're on?
            // ice might have a way lower rate, etc...
            const double RATE_OF_DECELERATION = 10;

            var maxVelocityAbs = GetMaxVelocityAbs();
            var minVelocityRange = -maxVelocityAbs;
            var maxVelocityRange = maxVelocityAbs;

            var velocityX = movementBehavior.VelocityX;
            var velocityY = movementBehavior.VelocityY;
            var throttleX = movementBehavior.ThrottleX;
            var throttleY = movementBehavior.ThrottleY;

            var velocityAdjustmentX = SPEED * throttleX;

            if (throttleX >= MIN_THROTTLE_ABS_VALUE ||
                throttleX < -1 * MIN_THROTTLE_ABS_VALUE)
            {
                velocityX = velocityX + velocityAdjustmentX;
            }
            else if (throttleX >= MIN_VELOCITY_ABS_VALUE)
            {
                velocityX = velocityX - RATE_OF_DECELERATION;
            }
            else if (throttleX < -1 * MIN_VELOCITY_ABS_VALUE)
            {
                velocityX = velocityX + RATE_OF_DECELERATION;
            }
            else
            {
                velocityX = 0;
            }

            velocityX = Math.Abs(velocityX) <= MIN_VELOCITY_ABS_VALUE
                ? 0
                : Math.Max(minVelocityRange, Math.Min(maxVelocityRange, velocityX));

            var velocityAdjustmentY = SPEED * throttleY;

            if (throttleY >= MIN_THROTTLE_ABS_VALUE ||
                throttleY < -1 * MIN_THROTTLE_ABS_VALUE)
            {
                velocityY = velocityY + velocityAdjustmentY;
            }
            else if (throttleY >= MIN_VELOCITY_ABS_VALUE)
            {
                velocityY = velocityY - RATE_OF_DECELERATION;
            }
            else if (throttleY < -1 * MIN_VELOCITY_ABS_VALUE)
            {
                velocityY = velocityY + RATE_OF_DECELERATION;
            }
            else
            {
                velocityY = 0;
            }

            velocityY = Math.Abs(velocityY) <= MIN_VELOCITY_ABS_VALUE
                ? 0
                : Math.Max(minVelocityRange, Math.Min(maxVelocityRange, velocityY));

            // walking on diagonals shouldn't make you go super fast
            if (Math.Abs(velocityX) > 0 &&
                Math.Abs(velocityY) > 0)
            {
                velocityX /= 1.41d;
                velocityY /= 1.41d;
            }

            return new Vector2((float)velocityX, (float)velocityY);
        }

        private float GetMaxVelocityAbs()
        {
            return 2f;
        }

        private Vector2 Lerp(Vector2 start, Vector2 end, float percent)
        {
            return start + percent * (end - start);
        }

        private void CombatTurnManager_CombatStarted(object sender, EventArgs e)
        {
            _tileRestrictedMovement = true;
        }

        private void CombatTurnManager_CombatEnded(object sender, EventArgs e)
        {
            _tileRestrictedMovement = false;
        }
    }
}
