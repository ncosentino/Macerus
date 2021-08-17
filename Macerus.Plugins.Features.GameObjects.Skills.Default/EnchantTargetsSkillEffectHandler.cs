using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation;
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

        public async Task<IGameObject> HandleAsync(
            IGameObject user,
            IGameObject skill,
            IGameObject skillEffect)
        {
            if (!skillEffect.TryGetFirst<IApplyEnchantmentsBehavior>(out var enchantTargetsBehavior))
            {
                return skillEffect;
            }

            var statefulEnchantments = _enchantmentLoader
                .LoadForEnchantmenDefinitionIds(enchantTargetsBehavior.EnchantmentDefinitionIds);
            var skillTargets = _skillTargetingAmenity.FindTargetsForSkillEffect(
                user,
                skillEffect);

            foreach (var target in skillTargets)
            {
                var targetEnchantmentsBehavior = target.GetOnly<IHasEnchantmentsBehavior>();
                await targetEnchantmentsBehavior
                    .AddEnchantmentsAsync(statefulEnchantments)
                    .ConfigureAwait(false);
                _logger.Debug($"{user} enchanted {target} using skill effect '{skillEffect}'.");
            }

            return skillEffect;
        }
    }
}
