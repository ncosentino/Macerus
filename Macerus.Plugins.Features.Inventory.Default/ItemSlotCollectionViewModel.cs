using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.Inventory.Api;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class ItemSlotCollectionViewModel :
        NotifierBase,
        IItemSlotCollectionViewModel
    {
        private readonly IDictionary<object, IItemSlotViewModel> _itemSlots;

        private bool _isDragOver;
        private bool _isDropAllowed;

        public ItemSlotCollectionViewModel()
        {
            _itemSlots = new ObservableConcurrentDictionary<object, IItemSlotViewModel>();
            _isDropAllowed = true;
        }

        public event EventHandler<DragStartEventArgs> DragStarted;

        public event EventHandler<DragEndEventArgs> DragEnded;

        public event EventHandler<DroppedEventArgs> Dropped;

        public IEnumerable<IItemSlotViewModel> ItemSlots => _itemSlots.Values; // FIXME: be able to wrap with filter/sort

        public string BackgroundImageResourceId { get; }

        public bool IsDragOver
        {
            get { return _isDragOver; }
            set
            {
                if (_isDragOver != value)
                {
                    _isDragOver = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsDropAllowed
        {
            get { return _isDropAllowed; }
            set
            {
                if (_isDropAllowed != value)
                {
                    _isDropAllowed = value;
                    OnPropertyChanged();
                }
            }
        }

        public void SetItemSlots(IEnumerable<IItemSlotViewModel> itemSlots)
        {
            _itemSlots.Clear();
            foreach (var slot in itemSlots)
            {
                _itemSlots.Add(slot.Id, slot);
            }

            OnPropertyChanged(nameof(ItemSlots));
        }

        public IItemSlotViewModel this[string id]
        {
            get
            {
                return _itemSlots.TryGetValue(id, out var itemSlot)
                    ? itemSlot
                    : null;
            }
        }

        public bool CanStartDragItem(IItemSlotViewModel slot)
        {
            return slot.HasItem; // FIXME: delegate this out because maybe we want to prevent dragging for other reasons
        }

        public void StartDragItem(IItemSlotViewModel slot)
        {
            DragStarted?.Invoke(
                this,
                new DragStartEventArgs(
                    this,
                    slot));
        }

        public void EndDragItem(bool success)
        {
            DragEnded?.Invoke(
                this,
                new DragEndEventArgs(success));
        }

        public bool CanDropItem(IItemSlotViewModel targetItemSlotViewModel)
        {
            // FIXME: we want a facade here to determine if we can actually do
            // drapping based on the type/data that's coming in (i.e. inventory vs equipment)
            if (targetItemSlotViewModel == null)
            {
                return true;
            }

            return targetItemSlotViewModel.IsDropAllowed != false;
        }

        public void DropItem(IItemSlotViewModel targetItemSlotViewModel)
        {
            Dropped?.Invoke(
                this,
                new DroppedEventArgs(
                    this,
                    targetItemSlotViewModel));
        }
    }
}