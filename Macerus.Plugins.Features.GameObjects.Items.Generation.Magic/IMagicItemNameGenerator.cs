using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public interface IMagicItemNameGenerator
    {
        IHasInventoryDisplayName GenerateName(
            IGameObject baseItem,
            IReadOnlyCollection<IGameObject> enchantments);
    }
}