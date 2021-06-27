using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Skills;

using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class EnchantTargetsSkillEffectHandler : IDiscoverableSkillEffectHandler
    {
        private readonly ILogger _logger;
        private readonly IEnchantmentLoader _enchantmentLoader;
        private readonly ISkillTargetingAmenity _skillTargetingAmenity;

        public EnchantTargetsSkillEffectHandler(
            ILogger logger,
            IEnchantmentLoader enchantmentLoader,
            ISkillTargetingAmenity skillTargetingAmenity)
        {
            _logger = logger;
            _enchantmentLoader = enchantmentLoader;
            _skillTargetingAmenity = skillTargetingAmenity;
        }

        public int? Priority { get; } = int.MinValue;

        public async Task HandleAsync(
            IGameObject user,
            IGameObject skillEffect)
        {
            if (!skillEffect.TryGetFirst<IApplyEnchantmentsBehavior>(out var enchantTargetsBehavior))
            {
                return;
            }

            var statefulEnchantments = _enchantmentLoader
                .LoadForEnchantmenDefinitionIds(enchantTargetsBehavior.EnchantmentDefinitionIds);
            var skillTargets = _skillTargetingAmenity.FindTargetsForSkillEffect(
                user,
                skillEffect);

            foreach (var target in skillTargets)
            {
                var targetEnchantmentsBehavior = target.GetOnly<IHasEnchantmentsBehavior>();
                targetEnchantmentsBehavior.AddEnchantments(statefulEnchantments);
                _logger.Debug($"{user} enchanted {target} using skill effect '{skillEffect}'.");
            }
        }
    }
}
