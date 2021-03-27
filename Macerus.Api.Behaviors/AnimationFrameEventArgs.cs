using System;

using Macerus.Plugins.Features.Animations.Api;

namespace Macerus.Api.Behaviors
{
    public sealed class AnimationFrameEventArgs : EventArgs
    {
        public AnimationFrameEventArgs(
            ISpriteAnimationFrame currentFrame,
            IAnimationMultipliers animationMultipliers)
        {
            CurrentFrame = currentFrame;
            AnimationMultipliers = animationMultipliers;
        }

        public ISpriteAnimationFrame CurrentFrame { get; }

        public IAnimationMultipliers AnimationMultipliers { get; }
    }
}