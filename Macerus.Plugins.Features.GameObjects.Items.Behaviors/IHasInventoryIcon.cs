using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public interface IHasInventoryIcon : IBehavior
    {
        // FIXME: should be identifier
        string IconResource { get; }
    }
}