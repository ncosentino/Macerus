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

        private bool _ignoreBehaviorEvents;

        public BagItemSet(IItemContainerBehavior itemContainerBehavior)
        {
            _itemContainerBehavior = itemContainerBehavior;

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

        public void SwapItems(
            IIdentifier itemIdToSwapOut,
            IGameObject itemToSwapIn)
        {
            var itemToSwapOut = GetItem(itemIdToSwapOut);

            // no-op
            if (itemToSwapIn == itemToSwapOut)
            {
                return;
            }

            IgnoringBehaviorEvents(() =>
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
                    return;
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
            });
        }

        private void IgnoringBehaviorEvents(Action callback)
        {
            var recall = _ignoreBehaviorEvents;
            try
            {
                _ignoreBehaviorEvents = true;
                callback();
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
