using System;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.AI
{
    public sealed class IdleAITaskBehavior :
        BaseBehavior,
        IIdleAITaskBehavior
    {
        public IdleAITaskBehavior(
            double weight,
            TimeSpan remainingIdleTime,
            TimeSpan minimumTargetIdleTime,
            TimeSpan maximumTargetIdleTime)
        {
            Weight = weight;
            RemainingIdleTime = remainingIdleTime;
            MinimumTargetIdleTime = minimumTargetIdleTime;
            MaximumTargetIdleTime = maximumTargetIdleTime;
        }

        public double Weight { get; }

        public TimeSpan RemainingIdleTime { get; set; }

        public TimeSpan MinimumTargetIdleTime { get; }

        public TimeSpan MaximumTargetIdleTime { get; }
    }
}
