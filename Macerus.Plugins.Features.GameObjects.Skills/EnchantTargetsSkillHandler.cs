using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class EnchantTargetsSkillHandler : IDiscoverableSkillHandler
    {
        private readonly ILogger _logger;
        private readonly IEnchantmentLoader _enchantmentLoader;
        private readonly ISkillTargetingAmenity _skillTargetingAmenity;

        public EnchantTargetsSkillHandler(
            ILogger logger,
            IEnchantmentLoader enchantmentLoader,
            ISkillTargetingAmenity skillTargetingAmenity)
        {
            _logger = logger;
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

            var skillName = skill.GetOnly<IIdentifierBehavior>();
            var skillTargets = _skillTargetingAmenity.FindTargetsForSkill(
                user,
                skill);

            foreach (var target in skillTargets)
            {
                var targetEnchantmentsBehavior = target.GetOnly<IHasEnchantmentsBehavior>();
                targetEnchantmentsBehavior.AddEnchantments(statefulEnchantments);

                _logger.Debug($"{user} enchanted {target} using skill {skillName.Id}.");
            }
        }
    }
}
