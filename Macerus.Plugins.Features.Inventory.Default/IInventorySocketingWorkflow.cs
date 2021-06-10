using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public interface IInventorySocketingWorkflow
    {
        bool TrySocketItem(IItemContainerBehavior itemContainerBehavior, IGameObject itemToBePlacedInsideSocket, IGameObject itemToHaveSocketFilled);
    }
}