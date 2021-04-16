using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IItemSlotCollectionViewModel : INotifyPropertyChanged
    {
        event EventHandler<DragStartEventArgs> DragStarted;

        event EventHandler<DragEndEventArgs> DragEnded;

        event EventHandler<DroppedEventArgs> Dropped;

        IEnumerable<IItemSlotViewModel> ItemSlots { get; }

        bool IsDragOver { get; set; }

        bool IsDropAllowed { get; set; }

        string BackgroundImageResourceId { get; }
        
        IItemSlotViewModel this[string id] { get; }

        void SetItemSlots(IEnumerable<IItemSlotViewModel> itemSlots);

        bool CanStartDragItem(IItemSlotViewModel slot);

        void StartDragItem(IItemSlotViewModel slot);

        void EndDragItem(bool success);

        bool CanDropItem(IItemSlotViewModel targetItemSlotViewModel);

        void DropItem(IItemSlotViewModel targetItemSlotViewModel);
    }
}