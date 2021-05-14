using Macerus.Api.Behaviors;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors.Api
{
    public interface IDynamicAnimationBehaviorFactory
    {
        IDynamicAnimationBehavior Create(
            string sourcePattern,
            IIdentifier baseAnimationId,
            bool visible,
            int currentFrameIndex);
    }
}
