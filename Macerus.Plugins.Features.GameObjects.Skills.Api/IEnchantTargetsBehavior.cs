using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface IEnchantTargetsBehavior : IBehavior
    {
        // FIXME: it would be good to find a way to apply these enchantment
        // IDs to different targets based on a filter
        IReadOnlyCollection<IIdentifier> StatefulEnchantmentDefinitionIds { get; }
    }
}
