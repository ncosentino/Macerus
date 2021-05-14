using Macerus.Plugins.Features.Interactions.Api;

namespace Macerus.Plugins.Features.GameObjects.Containers.Api
{
    public interface IContainerInteractableBehavior : IInteractableBehavior
    {
        bool DestroyOnUse { get; }

        bool TransferItemsOnActivate { get; }
    }
}