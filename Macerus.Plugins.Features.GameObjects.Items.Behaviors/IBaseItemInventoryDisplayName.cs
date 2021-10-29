
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation
{
    public interface IBaseItemInventoryDisplayName : IHasInventoryDisplayName
    {
        IIdentifier BaseItemStringResourceId { get; }
    }
}
