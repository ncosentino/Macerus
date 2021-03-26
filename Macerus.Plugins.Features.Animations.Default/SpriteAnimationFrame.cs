
using Macerus.Plugins.Features.Animations.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Animations.Default
{
    public sealed class SpriteAnimationFrame : ISpriteAnimationFrame
    {
        public SpriteAnimationFrame(
            IIdentifier spriteSheetResourceId,
            IIdentifier spriteResourceId,
            bool flipVertical,
            bool flipHorizontal,
            float? durationInSeconds,
            IFrameColor color)
        {
            SpriteSheetResourceId = spriteSheetResourceId;
            SpriteResourceId = spriteResourceId;
            FlipVertical = flipVertical;
            FlipHorizontal = flipHorizontal;
            DurationInSeconds = durationInSeconds;
            Color = color;
        }

        public IIdentifier SpriteSheetResourceId { get; }

        public IIdentifier SpriteResourceId { get; }

        public bool FlipVertical { get; }

        public bool FlipHorizontal { get; }

        public float? DurationInSeconds { get; }

        public IFrameColor Color { get; }
    }
}
