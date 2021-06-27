using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Skills;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillTargetingAmenity : ISkillTargetingAmenity
    {
        private readonly ICombatTeamIdentifiers _combatTeamIdentifiers;
        private readonly IMapGameObjectManager _mapGameObjectManager;

        public SkillTargetingAmenity(
            ICombatTeamIdentifiers combatTeamIdentifiers,
            IMapGameObjectManager mapGameObjectManager)
        {
            _combatTeamIdentifiers = combatTeamIdentifiers;
            _mapGameObjectManager = mapGameObjectManager;
        }

        public IEnumerable<IGameObject> FindTargetsForSkillEffect(
            IGameObject user,
            IGameObject skill)
        {
            if (!skill.TryGetFirst<ITargetPatternBehavior>(out var targetPatternBehavior) ||
                !skill.TryGetFirst<ITargetOriginBehavior>(out var targetOriginBehavior) ||
                !skill.TryGetFirst<ITargetCombatTeamBehavior>(out var targetCombatTeam))
            {
                return Enumerable.Empty<IGameObject>();
            }

            var transformedSkillOrigin = GetSkillOriginFromUserDirection(
                user,
                targetOriginBehavior.OffsetFromCasterX,
                targetOriginBehavior.OffsetFromCasterY);

            var transformedPatternFromOrigin = targetPatternBehavior
                .LocationsOffsetFromOrigin
                .Select(x => GetSkillLocationFromOriginAndUserDirection(user, transformedSkillOrigin, x));

            var affectedPositions = new[] { transformedSkillOrigin }
                .Concat(transformedPatternFromOrigin)
                .ToArray();

            var targets = _mapGameObjectManager
                .GameObjects
                .Where(x => x.Get<ITypeIdentifierBehavior>().Any(y => y.TypeId.Equals(new StringIdentifier("actor"))))
                .Where(x => targetCombatTeam
                    .AffectedTeamIds
                    .Contains(new IntIdentifier((int)x
                        .GetOnly<IHasMutableStatsBehavior>()
                        .BaseStats[_combatTeamIdentifiers.CombatTeamStatDefinitionId])))
                .Where(x => affectedPositions.Contains(
                    Tuple.Create(
                        (int)Math.Round(x.GetOnly<IPositionBehavior>().X),
                        (int)Math.Round(x.GetOnly<IPositionBehavior>().Y))));

            return targets;
        }

        public Tuple<int, IEnumerable<Vector2>> FindTargetLocationsForSkillEffect(
            IGameObject user,
            IGameObject skillEffect)
        {
            if (!skillEffect.TryGetFirst<ITargetPatternBehavior>(out var targetPatternBehavior) ||
                !skillEffect.TryGetFirst<ITargetOriginBehavior>(out var targetOriginBehavior) ||
                !skillEffect.TryGetFirst<ITargetCombatTeamBehavior>(out var targetCombatTeam))
            {
                return Tuple.Create(-1, Enumerable.Empty<Vector2>());
            }

            var transformedSkillOrigin = GetSkillOriginFromUserDirection(
                user,
                targetOriginBehavior.OffsetFromCasterX,
                targetOriginBehavior.OffsetFromCasterY);

            var transformedPatternFromOrigin = targetPatternBehavior
                .LocationsOffsetFromOrigin
                .Select(x => GetSkillLocationFromOriginAndUserDirection(user, transformedSkillOrigin, x));

            var affectedPositions = new[] { transformedSkillOrigin }
                .Concat(transformedPatternFromOrigin)
                .ToArray();

            return Tuple.Create(
                (targetCombatTeam.AffectedTeamIds.Single() as IntIdentifier).Identifier,
                affectedPositions.Select(x => new Vector2(x.Item1, x.Item2)));
        }

        private Tuple<int, int> GetSkillOriginFromUserDirection(
            IGameObject user,
            int originOffsetX,
            int originOffsetY)
        {
            var userPosition = user.GetOnly<IPositionBehavior>();

            var direction = !user.TryGetFirst<IMovementBehavior>(out var userMovement)
                ? -1
                : userMovement.Direction;

            var transformedOriginOffset = Transform(
                direction,
                originOffsetX,
                originOffsetY);

            var skillPositionX = (int)Math.Round(userPosition.X) + transformedOriginOffset.Item1;
            var skillPositionY = (int)Math.Round(userPosition.Y) + transformedOriginOffset.Item2;

            return Tuple.Create(skillPositionX, skillPositionY);
        }

        private Tuple<int, int> GetSkillLocationFromOriginAndUserDirection(
            IGameObject user,
            Tuple<int, int> origin,
            Tuple<int, int> location)
        {
            var direction = !user.TryGetFirst<IMovementBehavior>(out var userMovement)
                ? -1
                : userMovement.Direction;

            var transformedLocation = Transform(
                direction,
                location.Item1,
                location.Item2);

            var skillOriginX = origin.Item1 + transformedLocation.Item1;
            var skillOriginY = origin.Item2 + transformedLocation.Item2;

            return Tuple.Create(skillOriginX, skillOriginY);
        }

        private Tuple<int, int> Transform(int direction, int x, int y)
        {
            // All skill targeting is written as if you were the caster.
            // ie, the caster is facing forward.
            if (direction == 1)
            {
                return Tuple.Create(x, y);
            }

            // Facing down
            if (direction == 3)
            {
                return Tuple.Create(-x, -y);
            }

            // Facing left
            if (direction == 0)
            {
                return Tuple.Create(-y, x);
            }

            // Facing right
            if (direction == 2)
            {
                return Tuple.Create(y, -x);
            }

            return Tuple.Create(x, y);
        }
    }
}
