﻿using System;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Inventory.Api.Crafting;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Inventory.Default.Crafting
{
    public sealed class CraftingController : ICraftingController
    {
        private readonly IMacerusActorIdentifiers _macerusActorIdentifiers;
        private readonly IBagItemSetFactory _bagItemSetFactory;
        private readonly ICraftingWindowViewModel _craftingWindowViewModel;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IItemSetController _itemSetController;
        private readonly IItemSlotCollectionViewModel _playerBagItemSlotCollectionViewModel;
        private readonly IItemToItemSlotViewModelConverter _bagToItemSlotViewModelConverter;

        private IItemSetToViewModelBinder _bagBinder;

        public CraftingController(
            IMacerusActorIdentifiers macerusActorIdentifiers,
            IBagItemSetFactory bagItemSetFactory,
            ICraftingWindowViewModel craftingWindowViewModel,
            IMapGameObjectManager mapGameObjectManager,
            IItemSetController itemSetController,
            IItemSlotCollectionViewModel playerBagItemSlotCollectionViewModel,
            IItemToItemSlotViewModelConverter bagToItemSlotViewModelConverter)
        {
            _macerusActorIdentifiers = macerusActorIdentifiers;
            _bagItemSetFactory = bagItemSetFactory;
            _craftingWindowViewModel = craftingWindowViewModel;
            _mapGameObjectManager = mapGameObjectManager;
            _itemSetController = itemSetController;
            _playerBagItemSlotCollectionViewModel = playerBagItemSlotCollectionViewModel;
            _bagToItemSlotViewModelConverter = bagToItemSlotViewModelConverter;

            _craftingWindowViewModel.Opened += PlayerInventoryViewModel_Opened;
            _craftingWindowViewModel.Closed += PlayerInventoryViewModel_Closed;
        }

        public delegate CraftingController Factory(
            IItemSlotCollectionViewModel playerBagItemSlotCollectionViewModel,
            IItemToItemSlotViewModelConverter bagToItemSlotViewModelConverter);

        public void OpenCraftingWindow() => _craftingWindowViewModel.Open();

        public void CloseCraftingWindow() => _craftingWindowViewModel.Close();

        public bool ToggleCraftingWindow()
        {
            if (_craftingWindowViewModel.IsOpen)
            {
                CloseCraftingWindow();
            }
            else
            {
                OpenCraftingWindow();
            }

            return _craftingWindowViewModel.IsOpen;
        }

        private void PlayerInventoryViewModel_Opened(
            object sender,
            EventArgs e)
        {
            Contract.Requires(
                _bagBinder == null,
                $"Expecting '{nameof(_bagBinder)}' to be null.");

            var player = _mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.Has<IPlayerControlledBehavior>());
            Contract.RequiresNotNull(
                player,
                $"Expecting to find game object on map with behavior '{typeof(IPlayerControlledBehavior)}'.");

            var craftingInventoryBehavior = player
                .Get<IItemContainerBehavior>()
                .FirstOrDefault(x => x.ContainerId.Equals(_macerusActorIdentifiers.CraftingInventoryIdentifier));
            Contract.RequiresNotNull(
                craftingInventoryBehavior,
                $"Expecting to find a matching behavior of type '{typeof(IItemContainerBehavior)}' on '{player}'.");

            _bagBinder = new ItemSetToViewModelBinder(
                _bagToItemSlotViewModelConverter,
                _bagItemSetFactory.Create(craftingInventoryBehavior),
                _playerBagItemSlotCollectionViewModel);

            _itemSetController.Register(_bagBinder);

            _bagBinder.RefreshViewModel();
        }

        private void PlayerInventoryViewModel_Closed(
            object sender,
            EventArgs e)
        {
            Contract.RequiresNotNull(
                _bagBinder,
                $"Expecting '{nameof(_bagBinder)}' to not be null.");

            _itemSetController.EndPendingDragDrop();
            _itemSetController.Unregister(_bagBinder);

            _bagBinder = null;
        }
    }
}