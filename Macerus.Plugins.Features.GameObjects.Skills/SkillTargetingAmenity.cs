using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
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
            if (!skill.TryGetFirst<ISkillTargetBehavior>(out var targetBehavior))
            {
                return Enumerable.Empty<IGameObject>();
            }

            var userLocation = user.GetOnly<IWorldLocationBehavior>();
            var skillOriginX = (int)Math.Round(userLocation.X) + targetBehavior.OriginOffset.Item1;
            var skillOriginY = (int)Math.Round(userLocation.Y) + targetBehavior.OriginOffset.Item2;

            var affectedLocations = new[] { Tuple.Create(skillOriginX, skillOriginY) }
                .Concat(targetBehavior
                    .PatternFromOrigin
                    .Select(x => Tuple.Create(skillOriginX + x.Item1, skillOriginY + x.Item2)))
                .ToArray();

            var targets = _mapGameObjectManager
                .GameObjects
                .Where(x => x.Get<ITypeIdentifierBehavior>().Any(y => y.TypeId.Equals(new StringIdentifier("actor"))))
                .Where(x => targetBehavior
                    .TeamIds
                    .Contains((int)x
                        .GetOnly<IHasMutableStatsBehavior>()
                        .BaseStats[_combatTeamIdentifiers.CombatTeamStatDefinitionId]))
                .Where(x => affectedLocations.Contains(
                    Tuple.Create(
                        (int)Math.Round(x.GetOnly<IWorldLocationBehavior>().X),
                        (int)Math.Round(x.GetOnly<IWorldLocationBehavior>().Y))));

            return targets;
        }
    }
}
