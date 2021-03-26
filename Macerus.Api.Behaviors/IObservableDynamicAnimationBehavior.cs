using System;

namespace Macerus.Api.Behaviors
{
    public interface IObservableDynamicAnimationBehavior : IReadOnlyDynamicAnimationBehavior
    {
        event EventHandler<AnimationFrameEventArgs> AnimationFrameChanged;
    }
}