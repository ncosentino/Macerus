using System.Linq;
using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class EnchantTargetsBehavior :
        BaseBehavior,
        IEnchantTargetsBehavior
    {
        public EnchantTargetsBehavior(
            params IIdentifier[] definitionIds) : this(definitionIds.ToList())
        {
        }

        public EnchantTargetsBehavior(
            IEnumerable<IIdentifier> definitionIds)
        {
            StatefulEnchantmentDefinitionIds = definitionIds.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> StatefulEnchantmentDefinitionIds { get; }
    }
}
