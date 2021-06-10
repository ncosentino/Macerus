using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public interface IRareItemNameGenerator
    {
        IHasInventoryDisplayName GenerateName(IEnumerable<IBehavior> itemBehaviors, IReadOnlyCollection<IGameObject> enchantments);
    }
}