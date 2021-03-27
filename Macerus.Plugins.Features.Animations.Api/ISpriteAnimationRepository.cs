using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Animations.Api
{
    public interface ISpriteAnimationRepository
    {
        ISpriteAnimation GetAnimationById(IIdentifier animationId);

        bool TryGetAnimationById(
            IIdentifier animationId,
            out ISpriteAnimation spriteAnimation);
    }
}
