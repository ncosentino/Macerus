using System;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.AI
{
    public interface IIdleAITaskBehavior : IAITaskBehavior
    {
        TimeSpan RemainingIdleTime { get; set; }

        TimeSpan MinimumTargetIdleTime { get; }

        TimeSpan MaximumTargetIdleTime { get; }
    }
}
