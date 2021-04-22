using ProjectXyz.Api.Behaviors;

namespace Macerus.Plugins.Features.Interactions.Api
{
    public interface IInteractableBehavior : IBehavior
    {
        bool AutomaticInteraction { get; }
    }
}