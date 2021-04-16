using System;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public sealed class DroppedEventArgs : EventArgs
    {
        public DroppedEventArgs(
            IItemSlotCollectionViewModel dropTargetInventory,
            IItemSlotViewModel dropTargetSlot)
        {
            DropTargetInventory = dropTargetInventory;
            DropTargetSlot = dropTargetSlot;
        }

        public IItemSlotCollectionViewModel DropTargetInventory { get; }

        public IItemSlotViewModel DropTargetSlot { get; }
    }
}