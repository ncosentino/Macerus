
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Animations.Api
{
    public interface ISpriteAnimationFrame
    {
        IFrameColor Color { get; }

        float? DurationInSeconds { get; }

        bool FlipHorizontal { get; }

        bool FlipVertical { get; }

        IIdentifier SpriteSheetResourceId { get; }

        IIdentifier SpriteResourceId { get; }
    }
}
