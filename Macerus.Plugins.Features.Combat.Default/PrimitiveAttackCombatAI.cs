using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class PrimitiveAttackCombatAI : ICombatAI
    {
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ISkillUsage _skillUsage;
        private readonly ISkillHandlerFacade _skillHandlerFacade;
        private readonly ICombatTeamIdentifiers _combatTeamIdentifiers;
        private readonly ILogger _logger;

        public PrimitiveAttackCombatAI(
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ISkillUsage skillUsage,
            ISkillHandlerFacade skillHandlerFacade,
            ICombatTeamIdentifiers combatTeamIdentifiers,
            ILogger logger)
        {
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _skillUsage = skillUsage;
            _skillHandlerFacade = skillHandlerFacade;
            _combatTeamIdentifiers = combatTeamIdentifiers;
            _logger = logger;
        }

        public void Execute(
            IGameObject actor,
            IReadOnlyCollection<IGameObject> combatGameObjects)
        {
            var skills = actor
                .GetOnly<IHasSkillsBehavior>()
                .Skills;
            var firstUsableSkill = skills.FirstOrDefault(x =>
                x.Has<IInflictDamageBehavior>() &&
                x.Has<IUseInCombatSkillBehavior>());
            if (firstUsableSkill == null)
            {
                _logger.Info(
                    $"'{actor}' does not have a usable skill for combat that " +
                    $"inflicts damage.");
                return;
            }

            if (!_skillUsage.CanUseSkill(
                actor,
                firstUsableSkill))
            {
                _logger.Info(
                    $"'{actor}' cannot use skill '{firstUsableSkill}'.");
                return;
            }

            var actorTeam = _statCalculationServiceAmenity.GetStatValue(
                actor,
                _combatTeamIdentifiers.CombatTeamStatDefinitionId);

            // FIXME: actually check targeting with the specific skill...
            // FIXME: actually pick a close target not just any?
            var target = combatGameObjects
                .FirstOrDefault(potentialTarget =>
                {
                    if (!potentialTarget.TryGetFirst<IHasMutableStatsBehavior>(out var targetStatsBehavior))
                    {
                        return false;
                    }

                    var targetTeam = _statCalculationServiceAmenity.GetStatValue(
                        potentialTarget,
                        _combatTeamIdentifiers.CombatTeamStatDefinitionId);
                    if (targetTeam == actorTeam)
                    {
                        return false;
                    }

                    var targetLife = _statCalculationServiceAmenity.GetStatValue(
                        potentialTarget,
                        new IntIdentifier(1)); // FIXME: no hardcoding! this is current life
                    if (targetLife <= 0)
                    {
                        return false;
                    }

                    return true;
                });
            if (target == null)
            {
                _logger.Info(
                    $"'{actor}' could not find a target to attack.");
                return;
            }

            _skillUsage.UseRequiredResources(
                actor,
                firstUsableSkill);

            _skillHandlerFacade.Handle(
                actor,
                firstUsableSkill,
                new[] { target });
            _logger.Info(
                $"'{actor}' used skill '{firstUsableSkill}' on '{target}'.");
        }
    }
}
