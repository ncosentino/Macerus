using System;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Containers.Api.LootDrops;
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
        private readonly IBagItemSetFactory _bagItemSetFactory;
        private readonly IPlayerInventoryViewModel _playerInventoryViewModel;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly ILootDropFactory _lootDropFactory;
        private readonly IItemSetController _itemSetController;
        private readonly IItemSlotCollectionViewModel _playerEquipmentItemSlotCollectionViewModel;
        private readonly IItemSlotCollectionViewModel _playerBagItemSlotCollectionViewModel;
        private readonly IItemSlotCollectionViewModel _dropToMapItemSlotCollectionViewModel;
        private readonly IItemToItemSlotViewModelConverter _bagToItemSlotViewModelConverter;
        private readonly IItemToItemSlotViewModelConverter _equipmentToItemSlotViewModelConverter;

        private IItemSetToViewModelBinder _equipmentBinder;
        private IItemSetToViewModelBinder _bagBinder;
        private IItemSetToViewModelBinder _dropToMapBinder;

        public PlayerInventoryController(
            IBagItemSetFactory bagItemSetFactory,
            IPlayerInventoryViewModel playerInventoryViewModel,
            IMapGameObjectManager mapGameObjectManager,
            ILootDropFactory lootDropFactory,
            IItemSetController itemSetController,
            IItemSlotCollectionViewModel playerEquipmentItemSlotCollectionViewModel,
            IItemSlotCollectionViewModel playerBagItemSlotCollectionViewModel,
            IItemSlotCollectionViewModel dropToMapItemSlotCollectionViewModel,
            IItemToItemSlotViewModelConverter equipmentToItemSlotViewModelConverter,
            IItemToItemSlotViewModelConverter bagToItemSlotViewModelConverter)
        {
            _bagItemSetFactory = bagItemSetFactory;
            _playerInventoryViewModel = playerInventoryViewModel;
            _mapGameObjectManager = mapGameObjectManager;
            _lootDropFactory = lootDropFactory;
            _itemSetController = itemSetController;
            _playerEquipmentItemSlotCollectionViewModel = playerEquipmentItemSlotCollectionViewModel;
            _playerBagItemSlotCollectionViewModel = playerBagItemSlotCollectionViewModel;
            _dropToMapItemSlotCollectionViewModel = dropToMapItemSlotCollectionViewModel;
            _equipmentToItemSlotViewModelConverter = equipmentToItemSlotViewModelConverter;
            _bagToItemSlotViewModelConverter = bagToItemSlotViewModelConverter;

            _playerInventoryViewModel.Opened += PlayerInventoryViewModel_Opened;
            _playerInventoryViewModel.Closed += PlayerInventoryViewModel_Closed;
        }

        public delegate PlayerInventoryController Factory(
            IItemSlotCollectionViewModel playerEquipmentItemSlotCollectionViewModel,
            IItemSlotCollectionViewModel playerBagItemSlotCollectionViewModel,
            IItemSlotCollectionViewModel dropToMapItemSlotCollectionViewModel,
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
            Contract.Requires(
                _dropToMapBinder == null,
                $"Expecting '{nameof(_dropToMapBinder)}' to be null.");

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
                _bagItemSetFactory.Create(playerInventoryBehavior),
                _playerBagItemSlotCollectionViewModel);
            _dropToMapBinder = new ItemSetToViewModelBinder(
                _bagToItemSlotViewModelConverter,
                new DropToMapItemSet(
                    _lootDropFactory,
                    _mapGameObjectManager),
                _dropToMapItemSlotCollectionViewModel);

            _itemSetController.Register(_equipmentBinder);
            _itemSetController.Register(_bagBinder);
            _itemSetController.Register(_dropToMapBinder);

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
            Contract.RequiresNotNull(
                _dropToMapBinder,
                $"Expecting '{nameof(_dropToMapBinder)}' to not be null.");

            _itemSetController.Unregister(_equipmentBinder);
            _itemSetController.Unregister(_bagBinder);
            _itemSetController.Unregister(_dropToMapBinder);

            _equipmentBinder = null;
            _bagBinder = null;
            _dropToMapBinder = null;
        }
    }
}