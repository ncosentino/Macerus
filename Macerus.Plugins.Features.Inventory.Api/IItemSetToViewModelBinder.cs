﻿using System;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IItemSetToViewModelBinder
    {
        event EventHandler<PopulateHoverCardFromItemEventArgs> RequestPopulateHoverCardContent;

        IItemSlotCollectionViewModel ItemSlotCollectionViewModel { get; }

        IItemSet ItemSet { get; }

        IGameObject GetItemForViewModelId<TIdentifier>(TIdentifier id);

        bool CanSwapItems<TIdentifier>(
            TIdentifier viewModelItemIdToSwapOut,
            IGameObject itemToSwapIn);

        SwapResult SwapItems<TIdentifier>(
            TIdentifier viewModelItemIdToSwapOut,
            IGameObject itemToSwapIn);

        void RefreshViewModel();
    }
}