using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Mapping.Api;
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

        public IEnumerable<IGameObject> FindTargetsForSkill(
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

            var affectedLocations = new[] { transformedSkillOrigin }
                .Concat(transformedPatternFromOrigin)
                .ToArray();

            var targets = _mapGameObjectManager
                .GameObjects
                .Where(x => x.Get<ITypeIdentifierBehavior>().Any(y => y.TypeId.Equals(new StringIdentifier("actor"))))
                .Where(x => targetCombatTeam
                    .AffectedTeams
                    .Contains(new IntIdentifier((int)x
                        .GetOnly<IHasMutableStatsBehavior>()
                        .BaseStats[_combatTeamIdentifiers.CombatTeamStatDefinitionId])))
                .Where(x => affectedLocations.Contains(
                    Tuple.Create(
                        (int)Math.Round(x.GetOnly<IWorldLocationBehavior>().X),
                        (int)Math.Round(x.GetOnly<IWorldLocationBehavior>().Y))));

            return targets;
        }

        private Tuple<int, int> GetSkillOriginFromUserDirection(
            IGameObject user,
            int originOffsetX,
            int originOffsetY)
        {
            var userLocation = user.GetOnly<IWorldLocationBehavior>();

            var direction = !user.TryGetFirst<IMovementBehavior>(out var userMovement)
                ? -1
                : userMovement.Direction;

            var transformedOriginOffset = Transform(
                direction,
                originOffsetX,
                originOffsetY);

            var skillLocationX = (int)Math.Round(userLocation.X) + transformedOriginOffset.Item1;
            var skillLocationY = (int)Math.Round(userLocation.Y) + transformedOriginOffset.Item2;

            return Tuple.Create(skillLocationX, skillLocationY);
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
