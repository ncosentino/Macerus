using System.Collections.Generic;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class InflictDamageSkillHandler : IDiscoverableSkillHandler
    {
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;

        public InflictDamageSkillHandler(ICombatStatIdentifiers combatStatIdentifiers)
        {
            _combatStatIdentifiers = combatStatIdentifiers;
        }

        public void Handle(
            IGameObject user,
            IGameObject skill,
            IReadOnlyCollection<IGameObject> targets)
        {
            if (!skill.TryGetFirst<IInflictDamageBehavior>(out var inflictDamageBehavior))
            {
                return;
            }

            foreach (var target in targets)
            {
                // FIXME: we'll actually want to calculate damage...
                // - elemental, spell, physical, evasion, armor, crit, resistances
                // - resource stealing
                // - status effects
                // - chance to hit
                var targetStatsBehavior = target.GetOnly<IHasMutableStatsBehavior>();
                targetStatsBehavior.MutateStats(targetStats => targetStats[_combatStatIdentifiers.CurrentLifeStatId] -= 10);
            }
        }
    }
}
