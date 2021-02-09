using Macerus.Api.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public class AnimationBehavior :
        BaseBehavior,
        IAnimationBehavior
    {
        public IIdentifier CurrentAnimationId { get; set; }
    }
}