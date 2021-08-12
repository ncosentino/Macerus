using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillUsage : ISkillUsage
    {
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ICombatTurnManager _combatTurnManager;
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly ILogger _logger;

        public SkillUsage(
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ICombatTurnManager combatTurnManager,
            IFilterContextProvider filterContextProvider,
            ILogger logger)
        {
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _combatTurnManager = combatTurnManager;
            _filterContextProvider = filterContextProvider;
            _logger = logger;
        }

        public async Task<bool> CanUseSkillAsync(
            IGameObject actor,
            IGameObject skill)
        {
            if (actor.TryGetFirst<IReadOnlyMovementBehavior>(out var movementBehavior) &&
                movementBehavior.IsMovementIntended())
            {
                return false;
            }

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
                var actualStatValues = await _statCalculationServiceAmenity.GetStatValuesAsync(
                    actor,
                    skillResourceUsageBehavior.StaticStatRequirements.Keys);

                foreach (var requiredResourceKvp in skillResourceUsageBehavior.StaticStatRequirements)
                {
                    var requiredStatDefinitionId = requiredResourceKvp.Key;
                    var requiredStatValue = requiredResourceKvp.Value;
                    var actualStatValue = actualStatValues[requiredStatDefinitionId];

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

        public async Task UseRequiredResourcesAsync(
            IGameObject actor,
            IGameObject skill)
        {
            if (!skill.TryGetFirst<ISkillResourceUsageBehavior>(out var skillResourceUsageBehavior) ||
                !skillResourceUsageBehavior.StaticStatRequirements.Any())
            {
                return;
            }

            var actorMutableStats = actor.GetOnly<IHasStatsBehavior>();
            await actorMutableStats
                .MutateStatsAsync(async stats =>
                {
                    foreach (var requiredResourceKvp in skillResourceUsageBehavior.StaticStatRequirements)
                    {
                        var requiredStatDefinitionId = requiredResourceKvp.Key;
                        var requiredStatValue = requiredResourceKvp.Value;

                        stats[requiredStatDefinitionId] -= requiredStatValue;
                    }
                })
                .ConfigureAwait(false);
        }
    }
}
