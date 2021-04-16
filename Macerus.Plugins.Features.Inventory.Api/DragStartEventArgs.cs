using System;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public sealed class DragStartEventArgs : EventArgs
    {
        public DragStartEventArgs(
            IItemSlotCollectionViewModel dragSourceInventory,
            IItemSlotViewModel dragSourceSlot)
        {
            DragSourceInventory = dragSourceInventory;
            DragSourceSlot = dragSourceSlot;
        }

        public IItemSlotCollectionViewModel DragSourceInventory { get; }

        public IItemSlotViewModel DragSourceSlot { get; }
    }
}