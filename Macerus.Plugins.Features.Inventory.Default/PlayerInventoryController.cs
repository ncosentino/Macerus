using System;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Inventory.Api.HoverCards;
using Macerus.Plugins.Features.Inventory.Default.HoverCards;
using Macerus.Plugins.Features.Mapping;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class PlayerInventoryController : IPlayerInventoryController
    {
        private readonly IMacerusActorIdentifiers _macerusActorIdentifiers;
        private readonly IBagItemSetFactory _bagItemSetFactory;
        private readonly IPlayerInventoryViewModel _playerInventoryViewModel;
        private readonly Lazy<IMappingAmenity> _lazyMappingAmenity;
        private readonly IItemSetController _itemSetController;
        private readonly IItemSlotCollectionViewModel _playerEquipmentItemSlotCollectionViewModel;
        private readonly IItemSlotCollectionViewModel _playerBagItemSlotCollectionViewModel;
        private readonly IItemToItemSlotViewModelConverter _bagToItemSlotViewModelConverter;
        private readonly IViewWelderFactory _viewWelderFactory;
        private readonly IHoverCardViewFactory _hoverCardViewFactory;
        private readonly IBehaviorsToHoverCardPartViewModelConverterFacade _behaviorsToHoverCardPartViewModelConverter;
        private readonly IObservableRosterManager _rosterManager;
        private readonly IItemToItemSlotViewModelConverter _equipmentToItemSlotViewModelConverter;

        private IItemSetToViewModelBinder _equipmentBinder;
        private IItemSetToViewModelBinder _bagBinder;

        public PlayerInventoryController(
            IMacerusActorIdentifiers macerusActorIdentifiers,
            IBagItemSetFactory bagItemSetFactory,
            IPlayerInventoryViewModel playerInventoryViewModel,
            Lazy<IMappingAmenity> lazyMappingAmenity,
            IItemSetController itemSetController,
            IItemSlotCollectionViewModel playerEquipmentItemSlotCollectionViewModel,
            IItemSlotCollectionViewModel playerBagItemSlotCollectionViewModel,
            IItemToItemSlotViewModelConverter equipmentToItemSlotViewModelConverter,
            IItemToItemSlotViewModelConverter bagToItemSlotViewModelConverter,
            IViewWelderFactory viewWelderFactory,
            IHoverCardViewFactory hoverCardViewFactory,
            IBehaviorsToHoverCardPartViewModelConverterFacade behaviorsToHoverCardPartViewModelConverter,
            IObservableRosterManager rosterManager)
        {
            _macerusActorIdentifiers = macerusActorIdentifiers;
            _bagItemSetFactory = bagItemSetFactory;
            _playerInventoryViewModel = playerInventoryViewModel;
            _lazyMappingAmenity = lazyMappingAmenity;
            _itemSetController = itemSetController;
            _playerEquipmentItemSlotCollectionViewModel = playerEquipmentItemSlotCollectionViewModel;
            _playerBagItemSlotCollectionViewModel = playerBagItemSlotCollectionViewModel;
            _equipmentToItemSlotViewModelConverter = equipmentToItemSlotViewModelConverter;
            _bagToItemSlotViewModelConverter = bagToItemSlotViewModelConverter;
            _viewWelderFactory = viewWelderFactory;
            _hoverCardViewFactory = hoverCardViewFactory;
            _behaviorsToHoverCardPartViewModelConverter = behaviorsToHoverCardPartViewModelConverter;
            _rosterManager = rosterManager;
            _playerInventoryViewModel.Opened += PlayerInventoryViewModel_Opened;
            _playerInventoryViewModel.Closed += PlayerInventoryViewModel_Closed;
            _rosterManager.ControlledActorChanged += RosterManager_ControlledActorChanged;
        }

        public delegate PlayerInventoryController Factory(
            IItemSlotCollectionViewModel playerEquipmentItemSlotCollectionViewModel,
            IItemSlotCollectionViewModel playerBagItemSlotCollectionViewModel,
            IItemToItemSlotViewModelConverter equipmentToItemSlotViewModelConverter,
            IItemToItemSlotViewModelConverter bagToItemSlotViewModelConverter);

        public void OpenInventory() => _playerInventoryViewModel.Open();

        public void CloseInventory() => _playerInventoryViewModel.Close();

        public bool ToggleInventory()
        {
            if (_playerInventoryViewModel.IsOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }

            return _playerInventoryViewModel.IsOpen;
        }

        private void Recreate()
        {
            Contract.Requires(
                _equipmentBinder == null,
                $"Expecting '{nameof(_equipmentBinder)}' to be null.");
            Contract.Requires(
                _bagBinder == null,
                $"Expecting '{nameof(_bagBinder)}' to be null.");

            var player = _lazyMappingAmenity.Value.GetActivePlayerControlled();
            var playerEquipmentBehavior = player.GetOnly<ICanEquipBehavior>();
            var playerInventoryBehavior = player
                .Get<IItemContainerBehavior>()
                .FirstOrDefault(x => x.ContainerId.Equals(_macerusActorIdentifiers.InventoryIdentifier));
            Contract.RequiresNotNull(
                playerInventoryBehavior,
                $"Expecting to find a matching behavior of type '{typeof(IItemContainerBehavior)}' on '{player}'.");

            _equipmentBinder = new ItemSetToViewModelBinder(
                _equipmentToItemSlotViewModelConverter,
                new EquipmentItemSet(playerEquipmentBehavior),
                _playerEquipmentItemSlotCollectionViewModel);
            _equipmentBinder.RequestPopulateHoverCardContent += ItemBinder_RequestPopulateHoverCardContent;

            _bagBinder = new ItemSetToViewModelBinder(
                _bagToItemSlotViewModelConverter,
                _bagItemSetFactory.Create(playerInventoryBehavior),
                _playerBagItemSlotCollectionViewModel);
            _bagBinder.RequestPopulateHoverCardContent += ItemBinder_RequestPopulateHoverCardContent;

            _itemSetController.Register(_equipmentBinder);
            _itemSetController.Register(_bagBinder);

            _equipmentBinder.RefreshViewModel();
            _bagBinder.RefreshViewModel();
        }

        private void TearDown()
        {
            Contract.RequiresNotNull(
                _equipmentBinder,
                $"Expecting '{nameof(_equipmentBinder)}' to not be null.");
            Contract.RequiresNotNull(
                _bagBinder,
                $"Expecting '{nameof(_bagBinder)}' to not be null.");

            _itemSetController.EndPendingDragDrop();
            _itemSetController.Unregister(_equipmentBinder);
            _itemSetController.Unregister(_bagBinder);

            _equipmentBinder.RequestPopulateHoverCardContent -= ItemBinder_RequestPopulateHoverCardContent;
            _equipmentBinder = null;

            _bagBinder.RequestPopulateHoverCardContent -= ItemBinder_RequestPopulateHoverCardContent;
            _bagBinder = null;
        }

        private void RosterManager_ControlledActorChanged(
            object sender,
            EventArgs e)
        {
            if (_playerInventoryViewModel.IsOpen)
            {
                TearDown();
                Recreate();
            }
        }

        private void PlayerInventoryViewModel_Opened(
            object sender,
            EventArgs e)
        {
            Recreate();   
        }

        private void ItemBinder_RequestPopulateHoverCardContent(
            object sender,
            PopulateHoverCardFromItemEventArgs e)
        {
            var parts = _behaviorsToHoverCardPartViewModelConverter.Convert(e.Item.Behaviors);
            var viewModel = new HoverCardViewModel(parts);
            var hoverCardView = _hoverCardViewFactory.Create(viewModel);
            _viewWelderFactory
                .Create<ISimpleWelder>(
                    e.HoverCardContent,
                    hoverCardView)
                .Weld();
        }

        private void PlayerInventoryViewModel_Closed(
            object sender,
            EventArgs e)
        {
            TearDown();
        }
    }
}