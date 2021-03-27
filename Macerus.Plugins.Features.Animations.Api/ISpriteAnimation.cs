using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Animations.Api
{
    public interface ISpriteAnimation
    {
        IIdentifier Id { get; }

        IReadOnlyList<ISpriteAnimationFrame> Frames { get; }

        bool Repeat { get; }
    }
}
