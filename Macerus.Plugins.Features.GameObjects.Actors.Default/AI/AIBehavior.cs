using System.Collections.Concurrent;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.AI
{
    public sealed class AIBehavior :
        BaseBehavior,
        IAIBehavior
    {
        public AIBehavior()
        {
            Tasks = new ConcurrentQueue<IAITaskBehavior>();
            TasksToInitialize = new ConcurrentQueue<IAITaskBehavior>();
        }

        public ConcurrentQueue<IAITaskBehavior> Tasks { get; }

        public ConcurrentQueue<IAITaskBehavior> TasksToInitialize { get; }
    }
}
