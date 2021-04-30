using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class EnchantTargetsSkillHandler : IDiscoverableSkillHandler
    {
        private readonly IEnchantmentLoader _enchantmentLoader;
        private readonly ISkillTargetingAmenity _skillTargetingAmenity;

        public EnchantTargetsSkillHandler(
            IEnchantmentLoader enchantmentLoader,
            ISkillTargetingAmenity skillTargetingAmenity)
        {
            _enchantmentLoader = enchantmentLoader;
            _skillTargetingAmenity = skillTargetingAmenity;
        }

        public void Handle(
            IGameObject user,
            IGameObject skill)
        {
            if (!skill.TryGetFirst<IEnchantTargetsBehavior>(out var enchantTargetsBehavior))
            {
                return;
            }

            var statefulEnchantments = _enchantmentLoader
                .LoadForEnchantmenDefinitionIds(enchantTargetsBehavior.StatefulEnchantmentDefinitionIds);

            // FIXME: we need to answer:
            // - which targets of our full set of targets are applicable here?
            //   probably by default we might expect "all", but consider
            //   friendly-only, enemy-only, friendly-fire on, or even something
            //   more exotic like different targeted areas of the skill get
            //   different effects?
            // - See point above, but we probably want a way where we can apply
            //   specific enchantments to specific targets. i envision
            //   something that's like "buff allies, debuff enemies in radius"
            var skillTargets = _skillTargetingAmenity.FindTargetsForSkill(
                user,
                skill);

            foreach (var target in skillTargets)
            {
                var targetEnchantmentsBehavior = target.GetOnly<IHasEnchantmentsBehavior>();
                targetEnchantmentsBehavior.AddEnchantments(statefulEnchantments);
            }
        }
    }
}
