﻿using System.Linq;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillUsage : ISkillUsage
    {
        private readonly ISkillAmenity _skillAmenity;
        private readonly IStatCalculationService _statCalculationService;
        private readonly ILogger _logger;

        public SkillUsage(
            ISkillAmenity skillAmenity,
            IStatCalculationService statCalculationService,
            ILogger logger)
        {
            _skillAmenity = skillAmenity;
            _statCalculationService = statCalculationService;
            _logger = logger;
        }

        public bool CanUseSkill(
            IGameObject actor,
            IGameObject skill)
        {
            if (skill.TryGetFirst<ISkillResourceUsageBehavior>(out var skillResourceUsageBehavior) &&
                skillResourceUsageBehavior.StaticStatRequirements.Any())
            {
                foreach (var requiredResourceKvp in skillResourceUsageBehavior.StaticStatRequirements)
                {
                    var requiredStatDefinitionId = requiredResourceKvp.Key;
                    var requiredStatValue = requiredResourceKvp.Value;

                    var actualStatValue = _statCalculationService.GetStatValue(
                        actor,
                        requiredStatDefinitionId);

                    if (actualStatValue < requiredStatValue)
                    {
                        _logger.Debug(
                            $"'{actor}' did not meet required stat ID " +
                            $"'{requiredStatDefinitionId}' value of " +
                            $"{requiredStatValue}. Had value of " +
                            $"{actualStatValue}.");
                        return false;
                    }
                }
            }

            return true;
        }

        public void UseRequiredResources(
            IGameObject actor,
            IGameObject skill)
        {
            if (!skill.TryGetFirst<ISkillResourceUsageBehavior>(out var skillResourceUsageBehavior) ||
                !skillResourceUsageBehavior.StaticStatRequirements.Any())
            {
                return;
            }

            var actorMutableStats = actor.GetOnly<IHasMutableStatsBehavior>();
            actorMutableStats.MutateStats(stats =>
            {
                foreach (var requiredResourceKvp in skillResourceUsageBehavior.StaticStatRequirements)
                {
                    var requiredStatDefinitionId = requiredResourceKvp.Key;
                    var requiredStatValue = requiredResourceKvp.Value;

                    stats[requiredStatDefinitionId] -= requiredStatValue;
                }
            });
        }

        public void ApplySkillEffectsToTarget(
            IGameObject skill,
            IGameObject target)
        {
            var targetEnchantmentsBehavior = target.GetOnly<IHasEnchantmentsBehavior>();
            var skillDefinitionId = skill
                .GetOnly<ITemplateIdentifierBehavior>()
                .TemplateId;
            var statefulEnchantments = _skillAmenity.GetStatefulEnchantmentsBySkillId(skillDefinitionId);
            targetEnchantmentsBehavior.AddEnchantments(statefulEnchantments);
        }
    }
}
