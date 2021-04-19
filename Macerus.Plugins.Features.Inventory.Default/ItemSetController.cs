using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.Inventory.Api;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class ItemSetController : IItemSetController
    {
        private readonly Dictionary<IItemSlotCollectionViewModel, IItemSetToViewModelBinder> _viewModelToBinders;
        private readonly Dictionary<IItemSet, IItemSetToViewModelBinder> _itemSetToBinders;
        private readonly IItemDragViewModel _itemDragViewModel;
        
        private IItemSetToViewModelBinder _dragSourceBinder;

        public ItemSetController(IItemDragViewModel itemDragViewModel)
        {
            _viewModelToBinders = new Dictionary<IItemSlotCollectionViewModel, IItemSetToViewModelBinder>();
            _itemSetToBinders = new Dictionary<IItemSet, IItemSetToViewModelBinder>();
            _itemDragViewModel = itemDragViewModel;
        }

        public void Register(IItemSetToViewModelBinder binder)
        {
            var viewModel = binder.ItemSlotCollectionViewModel;
            var itemSet = binder.ItemSet;

            _viewModelToBinders[viewModel] = binder;
            _itemSetToBinders[itemSet] = binder;

            viewModel.DragStarted += ItemSlotCollectionViewModel_DragStarted;
            viewModel.DragEnded += ItemSlotCollectionViewModel_DragEnded;
            viewModel.Dropped += ItemSlotCollectionViewModel_Dropped;

            itemSet.ItemsChanged += ItemSet_ItemsChanged;
        }

        public void Unregister(IItemSetToViewModelBinder binder)
        {
            var viewModel = binder.ItemSlotCollectionViewModel;
            var itemSet = binder.ItemSet;

            _viewModelToBinders.Remove(viewModel);
            _itemSetToBinders.Remove(itemSet);

            viewModel.DragStarted -= ItemSlotCollectionViewModel_DragStarted;
            viewModel.DragEnded -= ItemSlotCollectionViewModel_DragEnded;
            viewModel.Dropped -= ItemSlotCollectionViewModel_Dropped;

            itemSet.ItemsChanged -= ItemSet_ItemsChanged;
        }

        private void UpdateDropSlots()
        {
            var dragItem = _dragSourceBinder == null
                ? null
                : _dragSourceBinder.GetItemForViewModelId(_itemDragViewModel.DraggedItemSlot.Id);

            foreach (var entry in _viewModelToBinders)
            {
                var viewModel = entry.Key;
                var binder = entry.Value;

                foreach (var itemSlot in viewModel.ItemSlots)
                {
                    itemSlot.IsDropAllowed = binder.CanSwapItems(
                        itemSlot.Id,
                        dragItem);
                }
            }
        }

        private void ItemSlotCollectionViewModel_DragStarted(
            object sender,
            DragStartEventArgs e)
        {
            _dragSourceBinder = _viewModelToBinders[e.DragSourceInventory];
            _itemDragViewModel.DraggedItemSlot = e.DragSourceSlot;
            UpdateDropSlots();
        }

        private void ItemSlotCollectionViewModel_DragEnded(
            object sender,
            DragEndEventArgs e)
        {
            _itemDragViewModel.DraggedItemSlot = null;
            _dragSourceBinder = null;
            UpdateDropSlots();
        }

        private void ItemSlotCollectionViewModel_Dropped(
            object sender,
            DroppedEventArgs e)
        {
            var dropTargetBinder = _viewModelToBinders[e.DropTargetInventory];

            var dropTargetSlot = e.DropTargetSlot;
            var dragSourceSlot = _itemDragViewModel.DraggedItemSlot;

            var draggedItem = _dragSourceBinder.GetItemForViewModelId(dragSourceSlot.Id);
            var dropTargetItem = dropTargetSlot == null
                ? null
                : dropTargetBinder.GetItemForViewModelId(dropTargetSlot.Id);
            if (dropTargetBinder.CanSwapItems(dropTargetSlot?.Id, draggedItem) &&
                _dragSourceBinder.CanSwapItems(dragSourceSlot.Id, dropTargetItem))
            {
                var swapResult = dropTargetBinder.SwapItems(
                    dropTargetSlot?.Id,
                    draggedItem);
                if (swapResult == SwapResult.SuccessAndContinue)
                {
                    _dragSourceBinder.SwapItems(
                        dragSourceSlot.Id,
                        dropTargetItem);
                }
            }
        }

        private void ItemSet_ItemsChanged(
            object sender,
            EventArgs e)
        {
            var itemSet = (IItemSet)sender;
            var binder = _itemSetToBinders[itemSet];
            binder.RefreshViewModel();
        }
    }
}