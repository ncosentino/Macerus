using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public interface IRareItemNameGenerator
    {
        IHasInventoryDisplayName GenerateName(
            IEnumerable<IBehavior> itemBehaviors, 
            IReadOnlyCollection<IGameObject> enchantments,
            IFilterContext filterContext);
    }
}