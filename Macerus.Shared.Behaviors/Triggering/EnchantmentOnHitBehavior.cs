using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors.Triggering
{
    public sealed class EnchantmentOnHitBehavior : BaseBehavior
    {
        public EnchantmentOnHitBehavior(
            IEnumerable<IFilterAttributeValue> attackerFilter,
            IEnumerable<IFilterAttributeValue> defenderFilter,
            IEnumerable<IFilterAttributeValue> skillFilter,
            IEnumerable<IIdentifier> attackerEnchantmentDefinitionIds,
            IEnumerable<IIdentifier> defenderEnchantmentDefinitionIds,
            bool removeAfterTrigger)
        {
            AttackerFilter = attackerFilter.ToArray();
            DefenderFilter = defenderFilter.ToArray();
            SkillFilter = skillFilter.ToArray();
            AttackerEnchantmentDefinitionIds = attackerEnchantmentDefinitionIds.ToArray();
            DefenderEnchantmentDefinitionIds = defenderEnchantmentDefinitionIds.ToArray();
            RemoveAfterTrigger = removeAfterTrigger;
        }

        public IReadOnlyCollection<IFilterAttributeValue> DefenderFilter { get; }

        public IReadOnlyCollection<IFilterAttributeValue> SkillFilter { get; }

        public IReadOnlyCollection<IIdentifier> DefenderEnchantmentDefinitionIds { get; }

        public IReadOnlyCollection<IIdentifier> AttackerEnchantmentDefinitionIds { get; }

        public IReadOnlyCollection<IFilterAttributeValue> AttackerFilter { get; }

        public bool RemoveAfterTrigger { get; }
    }
}