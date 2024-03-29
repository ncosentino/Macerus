﻿using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Triggers;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Plugins.Features.GameObjects.Skills;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class InflictDamageSkillEffectHandler : IDiscoverableSkillEffectHandler
    {
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;
        private readonly ISkillTargetingAmenity _skillTargetingAmenity;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;
        private readonly ILogger _logger;
        private readonly IHitTriggerMechanicSource _hitTriggerMechanicSource;

        public InflictDamageSkillEffectHandler(
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

        public async Task<IGameObject> HandleAsync(
            IGameObject user,
            IGameObject skill,
            IGameObject skillEffect)
        {
            if (!skillEffect.TryGetFirst<IInflictDamageBehavior>(out var inflictDamageBehavior))
            {
                return skillEffect;
            }

            var userEnchantments = user.GetOnly<IHasEnchantmentsBehavior>();

            // FIXME: this is obviously not right and just for testing...
            // we'd need to get min/max of all damage types and resistances figured out
            var damageStats = _statCalculationServiceAmenity.GetStatValue(
                user,
                _combatStatIdentifiers.FireDamageMinStatId,
                _statCalculationContextFactory.Create(
                    Enumerable.Empty<IComponent>(),
                    userEnchantments.Enchantments));

            var skillTargets = _skillTargetingAmenity.FindTargetsForSkillEffect(
                user, 
                skillEffect);

            foreach (var target in skillTargets)
            {
                var resistStats = _statCalculationServiceAmenity.GetStatValue(
                    target,
                    _combatStatIdentifiers.FireResistStatId);

                var totalDamage = damageStats - resistStats;

                var targetStatsBehavior = target.GetOnly<IHasStatsBehavior>();
                await targetStatsBehavior
                    .MutateStatsAsync(async targetStats => targetStats[_combatStatIdentifiers.CurrentLifeStatId] -= totalDamage)
                    .ConfigureAwait(false);

                _logger.Debug($"{user} inflicted {totalDamage} ({damageStats} - {resistStats}) to {target} using skill effect '{skillEffect}'");
                await _hitTriggerMechanicSource
                    .ActorHitTriggeredAsync(
                        user,
                        target,
                        skillEffect)
                    .ConfigureAwait(false);
            }

            return skillEffect;
        }
    }
}
