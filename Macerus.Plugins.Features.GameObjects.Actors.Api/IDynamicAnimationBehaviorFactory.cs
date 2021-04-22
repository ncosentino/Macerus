using Macerus.Api.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors.Api
{
    public interface IDynamicAnimationBehaviorFactory
    {
        IDynamicAnimationBehavior Create(string sourcePattern);
    }
}
