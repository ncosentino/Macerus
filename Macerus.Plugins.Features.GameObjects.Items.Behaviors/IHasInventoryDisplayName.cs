using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public interface IHasInventoryDisplayName : IBehavior
    {
        string DisplayName { get; }
    }
}