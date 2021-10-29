using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Unique
{
    public interface IUniqueItemInventoryDisplayName : IHasInventoryDisplayName
    {
        IIdentifier UniqueItemStringResourceId { get; }
    }
}
