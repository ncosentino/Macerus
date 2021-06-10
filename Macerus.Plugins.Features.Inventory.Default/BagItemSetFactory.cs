using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class BagItemSetFactory : IBagItemSetFactory
    {
        private readonly IInventorySocketingWorkflow _inventorySocketingWorkflow;

        public BagItemSetFactory(
            IInventorySocketingWorkflow inventorySocketingWorkflow)
        {
            _inventorySocketingWorkflow = inventorySocketingWorkflow;
        }

        public IItemSet Create(IItemContainerBehavior itemContainerBehavior)
        {
            var bagItemSet = new BagItemSet(
                itemContainerBehavior,
                _inventorySocketingWorkflow);
            return bagItemSet;
        }
    }
}
