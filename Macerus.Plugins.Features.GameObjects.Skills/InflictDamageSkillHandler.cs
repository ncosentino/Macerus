using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Triggers;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class InflictDamageSkillHandler : IDiscoverableSkillHandler
    {
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;
        private readonly ISkillTargetingAmenity _skillTargetingAmenity;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;
        private readonly ILogger _logger;
        private readonly IHitTriggerMechanicSource _hitTriggerMechanicSource;

        public InflictDamageSkillHandler(
            ICombatStatIdentifiers combatStatIdentifiers,
            ISkillTargetingAmenity skillTargetingAmenity,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            IStatCalculationContextFactory statCalculationContextFactory,
            ILogger logger,
            IHitTriggerMechanicSource hitTriggerMechanicSource)
        {
            _combatStatIdentifiers = combatStatIdentifiers;
            _skillTargetingAmenity = skillTargetingAmenity;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _statCalculationContextFactory = statCalculationContextFactory;
            _logger = logger;
            _hitTriggerMechanicSource = hitTriggerMechanicSource;
        }

        public int? Priority { get; } = null;

        public async Task HandleAsync(
            IGameObject user,
            IGameObject skill)
        {
            if (!skill.TryGetFirst<IInflictDamageBehavior>(out var inflictDamageBehavior))
            {
                return;
            }

            var userEnchantments = user.GetOnly<IHasEnchantmentsBehavior>();

            var damageStats = _statCalculationServiceAmenity.GetStatValue(
                user,
                new StringIdentifier("firedmg"),
                _statCalculationContextFactory.Create(
                    Enumerable.Empty<IComponent>(),
                    userEnchantments.Enchantments));

            var skillName = skill.GetOnly<IIdentifierBehavior>();
            var skillTargets = _skillTargetingAmenity.FindTargetsForSkill(
                user, 
                skill);

            foreach (var target in skillTargets)
            {
                var resistStats = _statCalculationServiceAmenity.GetStatValue(
                    target,
                    new StringIdentifier("fireresist"));

                var totalDamage = damageStats - resistStats;

                var targetStatsBehavior = target.GetOnly<IHasMutableStatsBehavior>();
                targetStatsBehavior.MutateStats(targetStats => targetStats[_combatStatIdentifiers.CurrentLifeStatId] -= totalDamage);

                _logger.Debug($"{user} inflicted {totalDamage} ({damageStats} - {resistStats}) to {target} using {skillName.Id}");
                await _hitTriggerMechanicSource
                    .ActorHitTriggeredAsync(
                        user,
                        target,
                        skill)
                    .ConfigureAwait(false);
            }
        }
    }
}
