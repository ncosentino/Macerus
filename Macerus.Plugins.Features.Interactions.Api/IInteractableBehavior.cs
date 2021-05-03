using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Interactions.Api
{
    public interface IInteractableBehavior : IBehavior
    {
        bool AutomaticInteraction { get; }
    }
}