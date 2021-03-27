using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Animations.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Animations.Default
{
    public sealed class SpriteAnimation : ISpriteAnimation
    {
        public SpriteAnimation(
            IIdentifier id,
            IEnumerable<ISpriteAnimationFrame> frames,
            bool repeat)
        {
            Id = id;
            Frames = frames.ToArray();
            Repeat = repeat;
        }

        public IIdentifier Id { get; }

        public IReadOnlyList<ISpriteAnimationFrame> Frames { get; }

        public bool Repeat { get; }
    }
}
