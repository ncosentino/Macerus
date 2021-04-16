using System;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Inventory.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class PlayerInventoryController : IPlayerInventoryController
    {
        private readonly IPlayerInventoryViewModel _playerInventoryViewModel;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IItemSetController _itemSetController;
        private readonly IItemSlotCollectionViewModel _playerEquipmentItemSlotCollectionViewModel;
        private readonly IItemSlotCollectionViewModel _playerBagItemSlotCollectionViewModel;
        private readonly IItemToItemSlotViewModelConverter _bagToItemSlotViewModelConverter;
        private readonly IItemToItemSlotViewModelConverter _equipmentToItemSlotViewModelConverter;

        private IItemSetToViewModelBinder _equipmentBinder;
        private IItemSetToViewModelBinder _bagBinder;

        public PlayerInventoryController(
            IPlayerInventoryViewModel playerInventoryViewModel,
            IMapGameObjectManager mapGameObjectManager,
            IItemSetController itemSetController,
            IItemSlotCollectionViewModel playerEquipmentItemSlotCollectionViewModel,
            IItemSlotCollectionViewModel playerBagItemSlotCollectionViewModel,
            IItemToItemSlotViewModelConverter equipmentToItemSlotViewModelConverter,
            IItemToItemSlotViewModelConverter bagToItemSlotViewModelConverter)
        {
            _playerInventoryViewModel = playerInventoryViewModel;
            _mapGameObjectManager = mapGameObjectManager;
            _itemSetController = itemSetController;
            _playerEquipmentItemSlotCollectionViewModel = playerEquipmentItemSlotCollectionViewModel;
            _playerBagItemSlotCollectionViewModel = playerBagItemSlotCollectionViewModel;
            _equipmentToItemSlotViewModelConverter = equipmentToItemSlotViewModelConverter;
            _bagToItemSlotViewModelConverter = bagToItemSlotViewModelConverter;

            _playerInventoryViewModel.Opened += PlayerInventoryViewModel_Opened;
            _playerInventoryViewModel.Closed += PlayerInventoryViewModel_Closed;
        }

        public delegate PlayerInventoryController Factory(
            IItemSlotCollectionViewModel playerEquipmentItemSlotCollectionViewModel,
            IItemSlotCollectionViewModel playerBagItemSlotCollectionViewModel,
            IItemToItemSlotViewModelConverter equipmentToItemSlotViewModelConverter,
            IItemToItemSlotViewModelConverter bagToItemSlotViewModelConverter);

        public void OpenInventory() => _playerInventoryViewModel.Open();

        public void CloseInventory() => _playerInventoryViewModel.Close();

        private void PlayerInventoryViewModel_Opened(
            object sender,
            EventArgs e)
        {
            Contract.Requires(
                _equipmentBinder == null,
                $"Expecting '{nameof(_equipmentBinder)}' to be null.");
            Contract.Requires(
                _bagBinder == null,
                $"Expecting '{nameof(_bagBinder)}' to be null.");

            var player = _mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.Has<IPlayerControlledBehavior>());
            Contract.RequiresNotNull(
                player,
                $"Expecting to find game object on map with behavior '{typeof(IPlayerControlledBehavior)}'.");

            var playerEquipmentBehavior = player.GetOnly<ICanEquipBehavior>();
            var playerInventoryBehavior = player
                .Get<IItemContainerBehavior>()
                .FirstOrDefault(x => x.ContainerId.Equals(new StringIdentifier("Inventory")));
            Contract.RequiresNotNull(
                playerInventoryBehavior,
                $"Expecting to find a matching behavior of type '{typeof(IItemContainerBehavior)}' on '{player}'.");

            _equipmentBinder = new ItemSetToViewModelBinder(
                _equipmentToItemSlotViewModelConverter,
                new EquipmentItemSet(playerEquipmentBehavior),
                _playerEquipmentItemSlotCollectionViewModel);
            _bagBinder = new ItemSetToViewModelBinder(
                _bagToItemSlotViewModelConverter,
                new BagItemSet(playerInventoryBehavior),
                _playerBagItemSlotCollectionViewModel);

            _itemSetController.Register(_equipmentBinder);
            _itemSetController.Register(_bagBinder);

            _equipmentBinder.RefreshViewModel();
            _bagBinder.RefreshViewModel();
        }

        private void PlayerInventoryViewModel_Closed(
            object sender,
            EventArgs e)
        {
            Contract.RequiresNotNull(
                _equipmentBinder,
                $"Expecting '{nameof(_equipmentBinder)}' to not be null.");
            Contract.RequiresNotNull(
                _bagBinder,
                $"Expecting '{nameof(_bagBinder)}' to not be null.");

            _itemSetController.Unregister(_equipmentBinder);
            _itemSetController.Unregister(_bagBinder);

            _equipmentBinder = null;
            _bagBinder = null;
        }
    }
}