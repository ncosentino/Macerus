using System;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Animations.Api
{
    public interface ISpriteAnimationProvider
    {
        ISpriteAnimation GetAnimationById(IIdentifier animationId);

        bool TryGetAnimationById(
            IIdentifier animationId,
            out ISpriteAnimation spriteAnimation);
    }
}
