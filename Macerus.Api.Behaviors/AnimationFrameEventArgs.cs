using System;

using Macerus.Plugins.Features.Animations.Api;

namespace Macerus.Api.Behaviors
{
    public sealed class AnimationFrameEventArgs : EventArgs
    {
        public AnimationFrameEventArgs(ISpriteAnimationFrame currentFrame)
        {
            CurrentFrame = currentFrame;
        }

        public ISpriteAnimationFrame CurrentFrame { get; }
    }
}