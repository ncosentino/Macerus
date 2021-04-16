using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class EquipmentItemSet : IItemSet
    {
        private readonly ICanEquipBehavior _canEquipBehavior;

        private bool _ignoreBehaviorEvents;

        public EquipmentItemSet(ICanEquipBehavior canEquipBehavior)
        {
            _canEquipBehavior = canEquipBehavior;
            _canEquipBehavior.Equipped += CanEquipBehavior_Equipped;
            _canEquipBehavior.Unequipped += CanEquipBehavior_Unequipped;
        }

        public event EventHandler<EventArgs> ItemsChanged;

        public IEnumerable<KeyValuePair<IIdentifier, IGameObject>> Items => _canEquipBehavior
            .SupportedEquipSlotIds
            .Select(x => new KeyValuePair<IIdentifier, IGameObject>(
                x,
                _canEquipBehavior.TryGet(x, out var canBeEquipped)
                    ? (IGameObject)canBeEquipped.Owner 
                    : null));

        public IGameObject GetItem(IIdentifier id)
        {
            if (id == null)
            {
                return null;
            }

            return _canEquipBehavior.TryGet(id, out var canBeEquipped)
                ? (IGameObject)canBeEquipped.Owner
                : null;
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

            // this is removing equipment
            if (itemToSwapIn == null)
            {
                // FIXME: do we ever need to double check if we can unequip?
                return true;
            }

            if (itemToSwapIn.TryGetFirst<ICanBeEquippedBehavior>(out var canBeEquippedBehavior) != true)
            {
                return false;
            }

            // this is either adding or swapping!

            // FIXME: we actually need to test if there's an item to unequip
            // at the same time as equipping. specifically, we need to understand
            // if we were to unequip the current item (if there is one), can
            // we still meet the requirements for this one?
            return _canEquipBehavior.CanEquip(
                itemIdToSwapOut,
                canBeEquippedBehavior,
                true);
        }

        public void SwapItems(
            IIdentifier itemIdToSwapOut,
            IGameObject itemToSwapIn)
        {
            var swapSlot = itemIdToSwapOut;
            var itemToSwapOut = GetItem(swapSlot);

            // no-op
            if (itemToSwapIn == itemToSwapOut)
            {
                return;
            }

            var canBeEquippedBehavior = itemToSwapIn?.GetOnly<ICanBeEquippedBehavior>();

            // prevent behavior changes from raising events while we take over
            IgnoringBehaviorEvents(() =>
            {
                // this is removing!
                if (itemToSwapIn == null)
                {
                    if (!_canEquipBehavior.TryUnequip(
                        swapSlot,
                        out var _))
                    {
                        throw new InvalidOperationException(
                            $"Expected to unequip '{itemToSwapOut}' from '{swapSlot}'.");
                    }

                    ItemsChanged?.Invoke(this, EventArgs.Empty);
                    return;
                }

                if (itemToSwapOut != null)
                {
                    if (!_canEquipBehavior.TryUnequip(
                        swapSlot,
                        out var _))
                    {
                        throw new InvalidOperationException(
                            $"Expected to unequip '{itemToSwapOut}' from '{swapSlot}'.");
                    }
                }

                if (canBeEquippedBehavior != null &&
                    !_canEquipBehavior.TryEquip(
                        swapSlot,
                        canBeEquippedBehavior,
                        true))
                {
                    throw new InvalidOperationException(
                        $"Expected to equip '{itemToSwapIn}' to '{swapSlot}'.");
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

        private void CanEquipBehavior_Unequipped(
            object sender,
            EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>> e)
        {
            if (_ignoreBehaviorEvents)
            {
                return;
            }

            ItemsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void CanEquipBehavior_Equipped(
            object sender,
            EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>> e)
        {
            if (_ignoreBehaviorEvents)
            {
                return;
            }

            ItemsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
