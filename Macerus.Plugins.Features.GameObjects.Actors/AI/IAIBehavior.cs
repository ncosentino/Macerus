using System.Collections.Concurrent;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.AI
{
    public interface IAIBehavior : IBehavior
    {
        ConcurrentQueue<IAITaskBehavior> Tasks { get; }

        ConcurrentQueue<IAITaskBehavior> TasksToInitialize { get; }
    }
}
