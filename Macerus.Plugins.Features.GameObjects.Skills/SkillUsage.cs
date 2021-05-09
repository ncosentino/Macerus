using System.Linq;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillUsage : ISkillUsage
    {
        private readonly IStatCalculationService _statCalculationService;
        private readonly ICombatTurnManager _combatTurnManager;
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly ILogger _logger;

        public SkillUsage(
            IStatCalculationService statCalculationService,
            ICombatTurnManager combatTurnManager,
            IFilterContextProvider filterContextProvider,
            ILogger logger)
        {
            _statCalculationService = statCalculationService;
            _combatTurnManager = combatTurnManager;
            _filterContextProvider = filterContextProvider;
            _logger = logger;
        }

        public bool CanUseSkill(
            IGameObject actor,
            IGameObject skill)
        {
            if (_combatTurnManager.InCombat)
            {
                if (!skill.Has<IUseInCombatBehavior>() ||
                    !_combatTurnManager.GetSnapshot(_filterContextProvider.GetContext(), 1).Single().Equals(actor))
                {
                    return false;
                }
            }
            else
            {
                if (!skill.Has<IUseOutOfCombatBehavior>())
                {
                    return false;
                }
            }

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
                        //_logger.Debug(
                        //    $"'{actor}' did not meet required stat ID " +
                        //    $"'{requiredStatDefinitionId}' value of " +
                        //    $"{requiredStatValue}. Had value of " +
                        //    $"{actualStatValue}.");
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
    }
}
