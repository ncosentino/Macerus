using ProjectXyz.Api.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public interface IHasInventoryDisplayName : IBehavior
    {
        string DisplayName { get; }
    }
}