
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.AI
{
    public interface IAITaskBehavior : IBehavior
    {
        double Weight { get; }
    }
}
