using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.Animations.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Content.Animations
{
    public sealed class SpriteAnimationRepository : 
        ISpriteAnimationRegistrar,
        ISpriteAnimationRepository
    {
        private readonly Dictionary<IIdentifier, ISpriteAnimation> _animations;

        public SpriteAnimationRepository()
        {
            _animations = new Dictionary<IIdentifier, ISpriteAnimation>();
        }

        public void Register(ISpriteAnimation spriteAnimation)
        {
            _animations.Add(spriteAnimation.Id, spriteAnimation);
        }

        public ISpriteAnimation GetAnimationById(IIdentifier animationId)
        {
            if (!TryGetAnimationById(
                animationId,
                out var spriteAnimation))
            {
                throw new InvalidOperationException(
                    $"Could not find animation with id '{animationId}'.");
            }

            return spriteAnimation;
        }

        public bool TryGetAnimationById(
            IIdentifier animationId,
            out ISpriteAnimation spriteAnimation)
        {
            if (!_animations.TryGetValue(
                animationId,
                out spriteAnimation))
            {
                return false;
            }

            return true;
        }
    }
}
