using Macerus.Api.Behaviors;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public interface IDynamicAnimationBehaviorFactory
    {
        IDynamicAnimationBehavior Create(
            IIdentifier baseAnimationId,
            bool visible,
            int currentFrameIndex);
    }
}
