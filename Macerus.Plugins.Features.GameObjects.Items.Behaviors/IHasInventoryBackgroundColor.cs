using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public interface IHasInventoryBackgroundColor : IBehavior
    {
        int R { get; }

        int G { get; }

        int B { get; }
    }
}