using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Animations.Api;

namespace Macerus.Plugins.Features.Animations.Default
{
    public sealed class SpriteAnimation : ISpriteAnimation
    {
        public SpriteAnimation(
            IEnumerable<ISpriteAnimationFrame> frames,
            bool repeat)
        {
            Frames = frames.ToArray();
            Repeat = repeat;
        }

        public IReadOnlyList<ISpriteAnimationFrame> Frames { get; }

        public bool Repeat { get; }
    }
}
