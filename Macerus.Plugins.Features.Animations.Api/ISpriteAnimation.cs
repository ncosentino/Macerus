using System.Collections.Generic;

namespace Macerus.Plugins.Features.Animations.Api
{
    public interface ISpriteAnimation
    {
        IReadOnlyList<ISpriteAnimationFrame> Frames { get; }

        bool Repeat { get; }
    }
}
