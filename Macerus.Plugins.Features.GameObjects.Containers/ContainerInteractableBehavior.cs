using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInteractableBehavior : 
        BaseBehavior,
        IInteractableBehavior
    {
        public ContainerInteractableBehavior(
            bool automaticInteraction)
        {
            AutomaticInteraction = automaticInteraction;
        }

        public bool AutomaticInteraction { get; }
    }
}
