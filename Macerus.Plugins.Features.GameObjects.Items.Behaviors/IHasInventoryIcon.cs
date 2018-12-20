using ProjectXyz.Api.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public interface IHasInventoryIcon : IBehavior
    {
        string IconResource { get; }
    }
}