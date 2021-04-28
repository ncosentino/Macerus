using Macerus.Api.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public class AnimationBehavior :
        BaseBehavior,
        IAnimationBehavior
    {
        public AnimationBehavior()
            : this(null, true)
        {
        }

        public AnimationBehavior(
            IIdentifier currentAnimationId,
            bool visible)
        {
            CurrentAnimationId = currentAnimationId;
            Visible = visible;
        }

        public IIdentifier CurrentAnimationId { get; set; }

        public bool Visible { get; set; }
    }
}