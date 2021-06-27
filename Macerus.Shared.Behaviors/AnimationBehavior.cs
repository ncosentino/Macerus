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
            IIdentifier baseAnimationId,
            bool visible)
        {
            BaseAnimationId = baseAnimationId;
            Visible = visible;
        }

        public IIdentifier BaseAnimationId { get; set; }

        public bool Visible { get; set; }
    }
}