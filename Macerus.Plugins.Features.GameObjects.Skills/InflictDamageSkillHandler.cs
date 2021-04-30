using System.Collections.Generic;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class InflictDamageSkillHandler : IDiscoverableSkillHandler
    {
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;
        private readonly ISkillTargetingAmenity _skillTargetingAmenity;

        public InflictDamageSkillHandler(
            ICombatStatIdentifiers combatStatIdentifiers,
            ISkillTargetingAmenity skillTargetingAmenity)
        {
            _combatStatIdentifiers = combatStatIdentifiers;
            _skillTargetingAmenity = skillTargetingAmenity;
        }

        public void Handle(
            IGameObject user,
            IGameObject skill)
        {
            if (!skill.TryGetFirst<IInflictDamageBehavior>(out var inflictDamageBehavior))
            {
                return;
            }

            var skillTargets = _skillTargetingAmenity.FindTargetsForSkill(
                user, 
                skill);

            foreach (var target in skillTargets)
            {
                // FIXME: we'll actually want to calculate damage...
                // - elemental, spell, physical, evasion, armor, crit, resistances
                // - resource stealing
                // - status effects
                // - chance to hit
                var targetStatsBehavior = target.GetOnly<IHasMutableStatsBehavior>();
                targetStatsBehavior.MutateStats(targetStats => targetStats[_combatStatIdentifiers.CurrentLifeStatId] -= 100);
            }
        }
    }
}
