using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Stats.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class PrimitiveAttackCombatAI : ICombatAI
    {
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly Lazy<ISkillUsage> _lazySkillUsage;
        private readonly Lazy<ISkillHandlerFacade> _lazySkillHandlerFacade;
        private readonly Lazy<ISkillAmenity> _lazySkillAmenity;
        private readonly ICombatTeamIdentifiers _combatTeamIdentifiers;
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;
        private readonly IMacerusActorIdentifiers _actorIdentifiers;
        private readonly Lazy<IMapProvider> _lazyMapProvider;
        private readonly ILogger _logger;

        private CombatState _combatState;
        private IGameObject _combatTarget;

        public PrimitiveAttackCombatAI(
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            Lazy<ISkillUsage> lazySkillUsage,
            Lazy<ISkillHandlerFacade> lazySkillHandlerFacade,
            Lazy<ISkillAmenity> lazySkillAmenity,
            ICombatTeamIdentifiers combatTeamIdentifiers,
            ICombatStatIdentifiers combatStatIdentifiers,
            IMacerusActorIdentifiers actorIdentifiers,
            Lazy<IMapProvider> lazyMapProvider,
            ILogger logger)
        {
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _lazySkillUsage = lazySkillUsage;
            _lazySkillHandlerFacade = lazySkillHandlerFacade;
            _lazySkillAmenity = lazySkillAmenity;
            _combatTeamIdentifiers = combatTeamIdentifiers;
            _combatStatIdentifiers = combatStatIdentifiers;
            _actorIdentifiers = actorIdentifiers;
            _lazyMapProvider = lazyMapProvider;
            _logger = logger;
        }

        public delegate PrimitiveAttackCombatAI Factory();

        private enum CombatState
        {
            Initial,
            IdentifyAttackTarget,
            IdentifyWalkTarget,
            WalkToTarget,
            UseSkill
        }

        public async Task<bool> ExecuteAsync(
            IGameObject actor,
            IReadOnlyCollection<IGameObject> combatGameObjects,
            IInterval<double> elapsed)
        {
            var turnShouldEnd = false;
            switch (_combatState)
            {
                case CombatState.Initial:
                    _combatState = CombatState.IdentifyAttackTarget;
                    turnShouldEnd = false;
                    break;
                case CombatState.IdentifyAttackTarget:
                    turnShouldEnd = PickAttackTarget(
                        actor,
                        combatGameObjects);
                    break;
                case CombatState.IdentifyWalkTarget:
                    turnShouldEnd = await PickWalkTargetAsync(
                        actor,
                        _combatTarget)
                        .ConfigureAwait(false);
                    break;
                case CombatState.WalkToTarget:
                    turnShouldEnd = WalkToTarget(
                        actor,
                        _combatTarget);
                    break;
                case CombatState.UseSkill:
                    turnShouldEnd = UseSkillOnTargetAsync(
                        actor,
                        _combatTarget).Result; // FIXME: propagate async pattern pls
                    break;
            }

            if (turnShouldEnd == true)
            {
                _combatState = CombatState.Initial;
            }

            return turnShouldEnd;
        }

        private async Task<bool> UseSkillOnTargetAsync(
            IGameObject actor,
            IGameObject target)
        {
            Contract.RequiresNotNull(actor, $"{nameof(actor)} cannot be null.");
            Contract.RequiresNotNull(target, $"{nameof(target)} cannot be null.");

            var skills = actor
                .GetOnly<IHasSkillsBehavior>()
                .Skills;
            var firstUsableSkill = skills
                .FirstOrDefault(s => _lazySkillAmenity
                    .Value
                    .GetSkillsFromCombination(s)
                    .Any(sc =>
                        sc.Has<IInflictDamageBehavior>() &&
                        sc.Has<IUseInCombatBehavior>()));
            if (firstUsableSkill == null)
            {
                _logger.Info(
                    $"'{actor}' does not have a usable skill for combat that " +
                    $"inflicts damage.");
                return true;
            }

            if (!await _lazySkillUsage.Value.CanUseSkillAsync(
                actor,
                firstUsableSkill))
            {
                _logger.Info(
                    $"'{actor}' cannot use skill '{firstUsableSkill}'.");
                return true;
            }

            _lazySkillUsage.Value.UseRequiredResources(
                actor,
                firstUsableSkill);

            await _lazySkillHandlerFacade
                .Value
                .HandleAsync(
                    actor,
                    firstUsableSkill)
                .ConfigureAwait(false);
            _logger.Info(
                $"'{actor}' used skill '{firstUsableSkill}' on '{target}'.");
            return true;
        }

        private bool PickAttackTarget(
            IGameObject actor, 
            IEnumerable<IGameObject> combatGameObjects)
        {
            _combatTarget = SelectTargetToAttack(
                actor,
                combatGameObjects);
            if (_combatTarget == null)
            {
                _logger.Info(
                    $"'{actor}' could not find a target to attack.");
                return true;
            }

            _logger.Info(
                $"'{actor}' is targeting '{_combatTarget}'.");
            _combatState = CombatState.IdentifyWalkTarget;
            return false;
        }

        private bool WalkToTarget(
            IGameObject actor,
            IGameObject target)
        {
            Contract.RequiresNotNull(actor, $"{nameof(actor)} cannot be null.");

            var movementBehavior = actor.GetOnly<IMovementBehavior>();
            if (movementBehavior.PointsToWalk.Count < 1)
            {
                // ensure we've stopped movin'
                _logger.Info($"'{actor}' has completed their walk path.");
                movementBehavior.SetThrottle(0, 0);

                // face our target
                movementBehavior.Direction = GetDirectionToFace(
                    actor,
                    target);

                _combatState = CombatState.UseSkill;
            }

            return false;
        }

        private int GetDirectionToFace(
            IGameObject actor,
            IGameObject target)
        {
            var actorPositionBehavior = actor.GetOnly<IReadOnlyPositionBehavior>();
            var targetPositionBehavior = target.GetOnly<IReadOnlyPositionBehavior>();
            var unitDirectionVector = Vector2.Normalize(
                new Vector2((float)targetPositionBehavior.X, (float)targetPositionBehavior.Y) -
                new Vector2((float)actorPositionBehavior.X, (float)actorPositionBehavior.Y));

            if (unitDirectionVector.X > 0)
            {
                return 2;
            }
            else if (unitDirectionVector.X < 0)
            {
                return 0;
            }
            else if (unitDirectionVector.Y > 0)
            {
                return 1;
            }
            else
            {
                return 3;
            }
        }

        private async Task<bool> PickWalkTargetAsync(
            IGameObject actor,
            IGameObject target)
        {
            Contract.RequiresNotNull(actor, $"{nameof(actor)} cannot be null.");
            Contract.RequiresNotNull(target, $"{nameof(target)} cannot be null.");

            var targetPositionBehavior = target.GetOnly<IReadOnlyPositionBehavior>();
            var targetLocation = new Vector2((float)targetPositionBehavior.X, (float)targetPositionBehavior.Y);
            var targetSizeBehavior = target.GetOnly<IReadOnlySizeBehavior>();
            var targetSize = new Vector2((float)targetSizeBehavior.Width, (float)targetSizeBehavior.Height);

            var pathFinder = _lazyMapProvider
                .Value
                .PathFinder;
            var allAdjacentPositions = pathFinder.GetAllAdjacentPositionsToObject(
                targetLocation,
                targetSize,
                true);

            var actorPositionBehavior = actor.GetOnly<IReadOnlyPositionBehavior>();
            var actorLocation = new Vector2((float)actorPositionBehavior.X, (float)actorPositionBehavior.Y);

            if (allAdjacentPositions.Any(x => Vector2.Distance(x, actorLocation) < double.Epsilon))
            {
                _logger.Info($"'{actor}' is already standing adjacent to target.");
                _combatState = CombatState.UseSkill;
                return false;
            }

            bool canMoveDiagonally = _statCalculationServiceAmenity.GetStatValue(
                actor,
                _actorIdentifiers.MoveDiagonallyStatDefinitionId) > 0;
            var allowedWalkDistance = _statCalculationServiceAmenity.GetStatValue(
                actor,
                _actorIdentifiers.MoveDistancePerTurnCurrentStatDefinitionId);

            var freeAdjacentPositions = pathFinder.GetFreeAdjacentPositionsToObject(
                targetLocation,
                targetSize,
                canMoveDiagonally);
            var destinationLocation = ClosestPosition(actorLocation, freeAdjacentPositions);

            _logger.Info(
                $"'{actor}' needs to walk from " +
                $"({actorLocation.X},{actorLocation.Y}) in order to " +
                $"({destinationLocation.X},{destinationLocation.Y}) in order to " +
                $"target '{target}' standing at position " +
                $"({targetLocation.X},{targetLocation.Y}).");

            var actorSizeBehavior = target.GetOnly<IReadOnlySizeBehavior>();
            var actorSize = new Vector2((float)actorSizeBehavior.Width, (float)actorSizeBehavior.Height);

            var canMoveToPositions = pathFinder
                .GetAllowedPathDestinations(
                    actorLocation,
                    actorSize,
                    allowedWalkDistance,
                    canMoveDiagonally)
                .ToArray();
            if (!canMoveToPositions.Contains(destinationLocation))
            {
                // pick the closest position to the target that the actor can move to
                var fallbackDestinationLocation = ClosestPosition(
                    targetLocation,
                    canMoveToPositions);

                _logger.Info(
                    $"'{actor}' cannot move to the desired destination location " +
                    $"({destinationLocation.X},{destinationLocation.Y}). They will move to " +
                    $"({fallbackDestinationLocation.X},{fallbackDestinationLocation.Y}).");
                destinationLocation = fallbackDestinationLocation;
            }

            var walkPath = await pathFinder
                .FindPathAsync(
                    actorLocation,
                    destinationLocation,
                    actorSize,
                    canMoveDiagonally)
                .ConfigureAwait(false);
            var pointsToWalk = new Queue<Vector2>(
                new[] { actorLocation }
                .Concat(walkPath.Positions));
            _logger.Info(
                $"Path:\r\n" +
                string.Join("\r\n", pointsToWalk.Select(p => $"\t({p.X},{p.Y})")));
            actor.GetOnly<IMovementBehavior>().SetWalkPath(pointsToWalk);
            actor
                .GetOnly<IHasMutableStatsBehavior>()
                .MutateStats(stats => stats[_actorIdentifiers.MoveDistancePerTurnCurrentStatDefinitionId] -= walkPath.TotalDistance);

            _combatState = CombatState.WalkToTarget;
            return false;
        }

        private Vector2 ClosestPosition(
            Vector2 source,
            IEnumerable<Vector2> candidates)
        {
            var closest = candidates
                .OrderBy(x => Math.Abs(source.X - x.X) + Math.Abs(source.Y - x.Y))
                .FirstOrDefault();
            return closest;
        }

        private IGameObject SelectTargetToAttack(
            IGameObject actor,
            IEnumerable<IGameObject> combatGameObjects)
        {
            var actorTeam = _statCalculationServiceAmenity.GetStatValue(
                actor,
                _combatTeamIdentifiers.CombatTeamStatDefinitionId);

            // FIXME: actually check targeting with the specific skill...
            // FIXME: actually pick a close target not just any?
            var target = combatGameObjects
                .FirstOrDefault(potentialTarget =>
                {
                    if (!potentialTarget.TryGetFirst<IHasMutableStatsBehavior>(out var targetStatsBehavior))
                    {
                        return false;
                    }

                    var targetTeam = _statCalculationServiceAmenity.GetStatValue(
                        potentialTarget,
                        _combatTeamIdentifiers.CombatTeamStatDefinitionId);
                    if (targetTeam == actorTeam)
                    {
                        return false;
                    }

                    // FIXME: can we just use the base stat for this?
                    var targetLife = _statCalculationServiceAmenity.GetStatValue(
                        potentialTarget,
                        _combatStatIdentifiers.CurrentLifeStatId);
                    if (targetLife <= 0)
                    {
                        return false;
                    }

                    return true;
                });
            return target;
        }
    }
}
