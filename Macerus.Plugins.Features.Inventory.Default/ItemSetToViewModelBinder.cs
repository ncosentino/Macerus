using System;
using System.Linq;

using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class ItemSetToViewModelBinder : IItemSetToViewModelBinder
    {
        private readonly IItemToItemSlotViewModelConverter _itemToItemSlotViewModelConverter;

        public ItemSetToViewModelBinder(
            IItemToItemSlotViewModelConverter itemToItemSlotViewModelConverter,
            IItemSet itemSet,
            IItemSlotCollectionViewModel itemSlotCollectionViewModel)
        {
            _itemToItemSlotViewModelConverter = itemToItemSlotViewModelConverter;
            ItemSet = itemSet;
            ItemSlotCollectionViewModel = itemSlotCollectionViewModel;
        }

        public IItemSlotCollectionViewModel ItemSlotCollectionViewModel { get; }

        public IItemSet ItemSet { get; }

        public IGameObject GetItemForViewModelId<TIdentifier>(TIdentifier viewModelId)
        {
            var backendId = ConvertViewModelToBackendId(viewModelId);
            return ItemSet.GetItem(backendId);
        }
        
        public bool CanSwapItems<TIdentifier>(
            TIdentifier viewModelItemIdToSwapOut,
            IGameObject itemToSwapIn)
        {
            var backendId = ConvertViewModelToBackendId(viewModelItemIdToSwapOut);
            var canSwap = ItemSet.CanSwapItems(backendId, itemToSwapIn);
            return canSwap;
        }

        public void SwapItems<TIdentifier>(
            TIdentifier viewModelItemIdToSwapOut,
            IGameObject itemToSwapIn)
        {
            var backendId = ConvertViewModelToBackendId(viewModelItemIdToSwapOut);
            ItemSet.SwapItems(backendId, itemToSwapIn);
        }

        public void RefreshViewModel()
        {
            var itemSlots = ItemSet
                .Items
                .Select(kvp => _itemToItemSlotViewModelConverter.Convert(
                    kvp.Key,
                    kvp.Value));
            ItemSlotCollectionViewModel.SetItemSlots(itemSlots);
        }

        private IIdentifier ConvertViewModelToBackendId(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (!(id is string))
            {
                throw new NotSupportedException(
                    $"Currently only support string view model IDs.");
            }

            return new StringIdentifier((string)id);
        }
    }
}