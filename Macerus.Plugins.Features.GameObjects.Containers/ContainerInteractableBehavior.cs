using Macerus.Plugins.Features.GameObjects.Containers.Api;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInteractableBehavior : 
        BaseBehavior,
        IContainerInteractableBehavior
    {
        public ContainerInteractableBehavior(
            bool automaticInteraction,
            bool destroyOnUse,
            bool transferItemsOnActivate)
        {
            AutomaticInteraction = automaticInteraction;
            DestroyOnUse = destroyOnUse;
            TransferItemsOnActivate = transferItemsOnActivate;
        }

        public bool AutomaticInteraction { get; }

        public bool DestroyOnUse { get; }

        public bool TransferItemsOnActivate { get; }
    }
}
