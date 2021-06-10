using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class BagItemSet : IItemSet
    {
        private readonly IItemContainerBehavior _itemContainerBehavior;
        private readonly IInventorySocketingWorkflow _inventorySocketingWorkflow;
        private bool _ignoreBehaviorEvents;

        public BagItemSet(
            IItemContainerBehavior itemContainerBehavior,
            IInventorySocketingWorkflow inventorySocketingWorkflow)
        {
            _itemContainerBehavior = itemContainerBehavior;
            _inventorySocketingWorkflow = inventorySocketingWorkflow;

            _itemContainerBehavior.ItemsChanged += ItemContainerBehavior_ItemsChanged;
        }

        public event EventHandler<EventArgs> ItemsChanged;

        public IEnumerable<KeyValuePair<IIdentifier, IGameObject>> Items => _itemContainerBehavior
            .Items
            .Select(x => new KeyValuePair<IIdentifier, IGameObject>(
                x.GetOnly<IIdentifierBehavior>().Id,
                x));

        public IGameObject GetItem(IIdentifier id)
        {
            if (id == null)
            {
                return null;
            }

            return _itemContainerBehavior
                .Items
                .FirstOrDefault(x => x
                    .GetOnly<IIdentifierBehavior>()
                    .Id
                    .Equals(id));
        }

        public bool CanSwapItems(
            IIdentifier itemIdToSwapOut,
            IGameObject itemToSwapIn)
        {
            var itemToSwapOut = GetItem(itemIdToSwapOut);

            // no-op
            if (itemToSwapIn == itemToSwapOut)
            {
                return true;
            }

            // this is adding!
            if (itemToSwapOut == null)
            {
                return _itemContainerBehavior.CanAddItem(itemToSwapIn);
            }

            // FIXME: this is a SWAP so we need to account for if we can
            // actually perform a swap (not just add and remove separately)
            return
                _itemContainerBehavior.CanRemoveItem(itemToSwapOut) &&
                (itemToSwapIn == null || _itemContainerBehavior.CanAddItem(itemToSwapIn));
        }

        public SwapResult SwapItems(
            IIdentifier itemIdToSwapOut,
            IGameObject itemToSwapIn)
        {
            var itemToSwapOut = GetItem(itemIdToSwapOut);

            // no-op
            if (itemToSwapIn == itemToSwapOut)
            {
                return SwapResult.SuccessAndStop;
            }

            return IgnoringBehaviorEvents(() =>
            {
                // this is adding!
                if (itemToSwapOut == null)
                {
                    if (!_itemContainerBehavior.TryAddItem(itemToSwapIn))
                    {
                        throw new InvalidOperationException(
                            $"Expected to add '{itemToSwapIn}' to '{_itemContainerBehavior}'.");
                    }

                    ItemsChanged?.Invoke(this, EventArgs.Empty);
                    return SwapResult.SuccessAndContinue;
                }

                var swapInIsSameBag =
                    itemToSwapIn != null &&
                    _itemContainerBehavior.Items.Any(x => x == itemToSwapIn);

                if (swapInIsSameBag &&
                    TryStackItems(
                        itemToSwapIn,
                        itemToSwapOut))
                {
                    ItemsChanged?.Invoke(this, EventArgs.Empty);
                    return SwapResult.SuccessAndStop;
                }

                if (swapInIsSameBag &&
                    _inventorySocketingWorkflow.TrySocketItem(
                        _itemContainerBehavior,
                        itemToSwapIn,
                        itemToSwapOut))
                {
                    ItemsChanged?.Invoke(this, EventArgs.Empty);
                    return SwapResult.SuccessAndStop;
                }

                // this is either removing or swapping!
                if (!_itemContainerBehavior.TryRemoveItem(itemToSwapOut))
                {
                    throw new InvalidOperationException(
                        $"Expected to remove '{itemToSwapOut}' from '{_itemContainerBehavior}'.");
                }

                if (itemToSwapIn != null && !_itemContainerBehavior.TryAddItem(itemToSwapIn))
                {
                    throw new InvalidOperationException(
                        $"Expected to add '{itemToSwapIn}' to '{_itemContainerBehavior}'.");
                }

                ItemsChanged?.Invoke(this, EventArgs.Empty);
                return SwapResult.SuccessAndContinue;
            });
        }

        private bool TryStackItems(
            IGameObject itemToBeAddedToStack,
            IGameObject itemWithStack)
        {
            if (itemWithStack == itemToBeAddedToStack)
            {
                return false;
            }

            if (!itemWithStack.TryGetFirst<IStackableItemBehavior>(out var existingItemStackBehavior))
            {
                return false;
            }

            if (!itemToBeAddedToStack.TryGetFirst<IStackableItemBehavior>(out var itemStackBehaviorToAdd))
            {
                return false;
            }

            if (!existingItemStackBehavior.StackId.Equals(itemStackBehaviorToAdd.StackId))
            {
                return false;
            }

            if (existingItemStackBehavior.StackLimit <= existingItemStackBehavior.Count)
            {
                return false;
            }

            var freeSpace = existingItemStackBehavior.StackLimit - existingItemStackBehavior.Count;
            var willAdd = Math.Min(freeSpace, itemStackBehaviorToAdd.Count);

            existingItemStackBehavior.Count += willAdd;
            itemStackBehaviorToAdd.Count -= willAdd;

            if (itemStackBehaviorToAdd.Count < 1)
            {
                if (!_itemContainerBehavior.TryRemoveItem(itemToBeAddedToStack))
                {
                    throw new InvalidOperationException(
                        $"'{itemStackBehaviorToAdd}' was fully added to " +
                        $"'{existingItemStackBehavior}' by moving {willAdd} " +
                        $"items. The source now has " +
                        $"{itemStackBehaviorToAdd.Count} but it could not be " +
                        $"removed from '{_itemContainerBehavior}'.");
                }
            }

            return true;
        }

        

        private SwapResult IgnoringBehaviorEvents(Func<SwapResult> callback)
        {
            var recall = _ignoreBehaviorEvents;
            try
            {
                _ignoreBehaviorEvents = true;
                var result = callback();
                return result;
            }
            finally
            {
                _ignoreBehaviorEvents = recall;
            }
        }

        private void ItemContainerBehavior_ItemsChanged(
            object sender,
            ItemsChangedEventArgs e)
        {
            if (_ignoreBehaviorEvents)
            {
                return;
            }
            
            ItemsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
