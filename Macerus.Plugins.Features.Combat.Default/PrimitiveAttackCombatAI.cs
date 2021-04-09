﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Stats;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class PrimitiveAttackCombatAI : ICombatAI
    {
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ISkillUsage _skillUsage;
        private readonly ISkillHandlerFacade _skillHandlerFacade;
        private readonly ICombatTeamIdentifiers _combatTeamIdentifiers;
        private readonly IMapProvider _mapProvider;
        private readonly ILogger _logger;

        private CombatState _combatState;
        private IGameObject _combatTarget;

        public PrimitiveAttackCombatAI(
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ISkillUsage skillUsage,
            ISkillHandlerFacade skillHandlerFacade,
            ICombatTeamIdentifiers combatTeamIdentifiers,
            IMapProvider mapProvider,
            ILogger logger)
        {
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _skillUsage = skillUsage;
            _skillHandlerFacade = skillHandlerFacade;
            _combatTeamIdentifiers = combatTeamIdentifiers;
            _mapProvider = mapProvider;
            _logger = logger;
        }

        private enum CombatState
        {
            Initial,
            IdentifyAttackTarget,
            IdentifyWalkTarget,
            WalkToTarget,
            UseSkill
        }

        public bool Execute(
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
                    turnShouldEnd = PickWalkTarget(
                        actor,
                        _combatTarget);
                    break;
                case CombatState.WalkToTarget:
                    turnShouldEnd = WalkToTarget(actor);
                    break;
                case CombatState.UseSkill:
                    turnShouldEnd = UseSkillOnTarget(
                        actor,
                        _combatTarget);
                    break;
            }

            if (turnShouldEnd == true)
            {
                _combatState = CombatState.Initial;
            }

            return turnShouldEnd;
        }

        private bool UseSkillOnTarget(
            IGameObject actor,
            IGameObject target)
        {
            Contract.RequiresNotNull(actor, $"{nameof(actor)} cannot be null.");
            Contract.RequiresNotNull(target, $"{nameof(target)} cannot be null.");

            var skills = actor
                .GetOnly<IHasSkillsBehavior>()
                .Skills;
            var firstUsableSkill = skills.FirstOrDefault(x =>
                x.Has<IInflictDamageBehavior>() &&
                x.Has<IUseInCombatSkillBehavior>());
            if (firstUsableSkill == null)
            {
                _logger.Info(
                    $"'{actor}' does not have a usable skill for combat that " +
                    $"inflicts damage.");
                return true;
            }

            if (!_skillUsage.CanUseSkill(
                actor,
                firstUsableSkill))
            {
                _logger.Info(
                    $"'{actor}' cannot use skill '{firstUsableSkill}'.");
                return true;
            }

            _skillUsage.UseRequiredResources(
                actor,
                firstUsableSkill);

            _skillHandlerFacade.Handle(
                actor,
                firstUsableSkill,
                new[] { target });
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

        private bool WalkToTarget(IGameObject actor)
        {
            Contract.RequiresNotNull(actor, $"{nameof(actor)} cannot be null.");

            var movementBehavior = actor.GetOnly<IMovementBehavior>();
            if (movementBehavior.PointsToWalk.Count < 1)
            {
                _logger.Info($"'{actor}' has completed their walk path.");

                movementBehavior.SetThrottle(0, 0);
                _combatState = CombatState.UseSkill;
            }

            return false;
        }

        private bool PickWalkTarget(
            IGameObject actor,
            IGameObject target)
        {
            Contract.RequiresNotNull(actor, $"{nameof(actor)} cannot be null.");
            Contract.RequiresNotNull(target, $"{nameof(target)} cannot be null.");

            var targetLocationBehavior = target.GetOnly<IWorldLocationBehavior>();
            var targetLocation = new Vector2((float)targetLocationBehavior.X, (float)targetLocationBehavior.Y);
            var adjacentPositions = _mapProvider.PathFinder.GetAdjacentPositions(
                targetLocation,
                true);
            var actorLocationBehavior = target.GetOnly<IWorldLocationBehavior>();
            var actorLocation = new Vector2((float)actorLocationBehavior.X, (float)actorLocationBehavior.Y);

            _logger.Info(
                $"'{actor}' needs to walk from " +
                $"({actorLocation.X},{actorLocation.Y}) in order to " +
                $"target '{target}' standing at position " +
                $"({targetLocation.X},{targetLocation.Y}).");

            var actorSize = new Vector2((float)actorLocationBehavior.Width, (float)actorLocationBehavior.Height);
            var pointsToWalk = new Queue<Vector2>(_mapProvider
                .PathFinder
                .FindPath(
                    actorLocation,
                    targetLocation,
                    actorSize));
            pointsToWalk.Enqueue(targetLocation);
            _logger.Info(
                $"Path:\r\n" +
                string.Join("\r\n", pointsToWalk.Select(p => $"\t({p.X},{p.Y})")));
            actor.GetOnly<IMovementBehavior>().SetWalkPath(pointsToWalk);

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

                    var targetLife = _statCalculationServiceAmenity.GetStatValue(
                        potentialTarget,
                        new IntIdentifier(1)); // FIXME: no hardcoding! this is current life
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
