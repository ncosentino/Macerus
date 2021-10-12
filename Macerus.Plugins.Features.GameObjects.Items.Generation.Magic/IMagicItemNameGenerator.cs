using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public interface IMagicItemNameGenerator
    {
        IHasInventoryDisplayName GenerateName(IEnumerable<IBehavior> itemBehaviors);
    }
}