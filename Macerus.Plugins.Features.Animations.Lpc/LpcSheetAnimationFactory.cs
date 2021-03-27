using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Animations.Api;
using Macerus.Plugins.Features.Animations.Default;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Animations.Lpc
{
    public sealed class LpcSheetAnimationFactory : ILpcSheetAnimationFactory
    {
        private enum Direction
        {
            Back,
            Left,
            Forward,
            Right
        }

        private static readonly IReadOnlyDictionary<Direction, int> _mapDirectionToMoveStartIndex = new Dictionary<Direction, int>()
        {
            [Direction.Back] = 60,
            [Direction.Left] = 69,
            [Direction.Forward] = 78,
            [Direction.Right] = 87,
        };

        public IEnumerable<ISpriteAnimation> CreateForSheet(IIdentifier spriteSheetResourceId)
        {
            foreach (var entry in CreateDirectionalAnimations(spriteSheetResourceId))
            {
                yield return entry;
            }
        }

        private IEnumerable<ISpriteAnimation> CreateDirectionalAnimations(IIdentifier spriteSheetResourceId)
        {
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var animationKey = $"{GetAnimationPrefix(spriteSheetResourceId)}_stand_{direction}".ToLowerInvariant();
                var animation = new SpriteAnimation(
                    new StringIdentifier(animationKey),
                    CreateStandAnimationFrame(
                        spriteSheetResourceId,
                        direction).Yield(),
                    true);
                yield return animation;

                foreach (var entry in CreateWalkAnimations(
                    spriteSheetResourceId,
                    direction))
                {
                    yield return entry;
                }
            }
        }

        private IEnumerable<ISpriteAnimation> CreateWalkAnimations(
            IIdentifier spriteSheetResourceId,
            Direction direction)
        {
            var animationPrefix = GetAnimationPrefix(spriteSheetResourceId);

            var frames = new List<ISpriteAnimationFrame>();
            foreach (var offset in Enumerable.Range(0, 9))
            {
                var frame = CreateWalkAnimationFrame(
                    spriteSheetResourceId,
                    direction,
                    offset);
                frames.Add(frame);
            }

            var animation = new SpriteAnimation(
                new StringIdentifier($"{animationPrefix}_walk_{direction}".ToLowerInvariant()),
                frames,
                true);
            yield return animation;
        }

        private ISpriteAnimationFrame CreateStandAnimationFrame(
            IIdentifier spriteSheetResourceId,
            Direction direction)
        {
            var index = _mapDirectionToMoveStartIndex[direction];
            var duration = 60f;
            return new SpriteAnimationFrame(
                spriteSheetResourceId,
                GetSpriteId(spriteSheetResourceId, index),
                false,
                false,
                duration,
                new FrameColor(1, 1, 1, 1));
        }

        private ISpriteAnimationFrame CreateWalkAnimationFrame(
            IIdentifier spriteSheetResourceId,
            Direction direction,
            int offset)
        {
            var index = _mapDirectionToMoveStartIndex[direction] + offset;
            var duration = 0.08f;
            return new SpriteAnimationFrame(
                spriteSheetResourceId,
                GetSpriteId(spriteSheetResourceId, index),
                false,
                false,
                duration,
                new FrameColor(1, 1, 1, 1));
        }

        private string GetAnimationPrefix(IIdentifier spriteSheetResourceId)
        {
            var resourceAsRelativePath = spriteSheetResourceId.ToString();
            var lastSeparatorIndex = resourceAsRelativePath.LastIndexOfAny(new char[] { '\\', '/' });
            var animationPrefix = resourceAsRelativePath.Substring(lastSeparatorIndex + 1);
            return animationPrefix;
        }

        private IIdentifier GetSpriteId(
            IIdentifier spriteSheetResourceId,
            int index)
        {
            var animationPrefix = GetAnimationPrefix(spriteSheetResourceId);
            var spriteId = new StringIdentifier($"{animationPrefix}_{index}");
            return spriteId;
        }
    }
}
