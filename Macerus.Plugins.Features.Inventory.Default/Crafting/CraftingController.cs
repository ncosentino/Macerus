using System;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Inventory.Api.Crafting;
using Macerus.Plugins.Features.Inventory.Api.HoverCards;
using Macerus.Plugins.Features.Inventory.Default.HoverCards;
using Macerus.Plugins.Features.Mapping;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Api;

namespace Macerus.Plugins.Features.Inventory.Default.Crafting
{
    public sealed class CraftingController : ICraftingController
    {
        private readonly IMacerusActorIdentifiers _macerusActorIdentifiers;
        private readonly ICraftingHandlerFacade _craftingHandler;
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly IBagItemSetFactory _bagItemSetFactory;
        private readonly ICraftingWindowViewModel _craftingWindowViewModel;
        private readonly IReadOnlyMappingAmenity _mappingAmenity;
        private readonly IItemSetController _itemSetController;
        private readonly IItemSlotCollectionViewModel _craftingBagItemSlotCollectionViewModel;
        private readonly IItemToItemSlotViewModelConverter _bagToItemSlotViewModelConverter;
        private readonly IViewWelderFactory _viewWelderFactory;
        private readonly IHoverCardViewFactory _hoverCardViewFactory;
        private readonly IBehaviorsToHoverCardPartViewModelConverterFacade _behaviorsToHoverCardPartViewModelConverter;
        private IItemSetToViewModelBinder _bagBinder;

        public CraftingController(
            IMacerusActorIdentifiers macerusActorIdentifiers,
            ICraftingHandlerFacade craftingHandler,
            IFilterContextProvider filterContextProvider,
            IBagItemSetFactory bagItemSetFactory,
            ICraftingWindowViewModel craftingWindowViewModel,
            IReadOnlyMappingAmenity mappingAmenity,
            IItemSetController itemSetController,
            IItemSlotCollectionViewModel craftingBagItemSlotCollectionViewModel,
            IItemToItemSlotViewModelConverter bagToItemSlotViewModelConverter,
            IViewWelderFactory viewWelderFactory,
            IHoverCardViewFactory hoverCardViewFactory,
            IBehaviorsToHoverCardPartViewModelConverterFacade behaviorsToHoverCardPartViewModelConverter)
        {
            _macerusActorIdentifiers = macerusActorIdentifiers;
            _craftingHandler = craftingHandler;
            _filterContextProvider = filterContextProvider;
            _bagItemSetFactory = bagItemSetFactory;
            _craftingWindowViewModel = craftingWindowViewModel;
            _mappingAmenity = mappingAmenity;
            _itemSetController = itemSetController;
            _craftingBagItemSlotCollectionViewModel = craftingBagItemSlotCollectionViewModel;
            _bagToItemSlotViewModelConverter = bagToItemSlotViewModelConverter;
            _viewWelderFactory = viewWelderFactory;
            _hoverCardViewFactory = hoverCardViewFactory;
            _behaviorsToHoverCardPartViewModelConverter = behaviorsToHoverCardPartViewModelConverter;
            _craftingWindowViewModel.Opened += CraftingWindowViewModel_Opened;
            _craftingWindowViewModel.Closed += CraftingWindowViewModel_Closed;
            _craftingWindowViewModel.RequestCraft += CraftingWindowViewModel_RequestCraft;
        }

        public delegate CraftingController Factory(
            IItemSlotCollectionViewModel craftingBagItemSlotCollectionViewModel,
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

        private void CraftingWindowViewModel_Opened(
            object sender,
            EventArgs e)
        {
            Contract.Requires(
                _bagBinder == null,
                $"Expecting '{nameof(_bagBinder)}' to be null.");

            var player = _mappingAmenity.GetActivePlayerControlled();
            var craftingInventoryBehavior = player
                .Get<IItemContainerBehavior>()
                .FirstOrDefault(x => x.ContainerId.Equals(_macerusActorIdentifiers.CraftingInventoryIdentifier));
            Contract.RequiresNotNull(
                craftingInventoryBehavior,
                $"Expecting to find a matching behavior of type '{typeof(IItemContainerBehavior)}' on '{player}'.");

            _bagBinder = new ItemSetToViewModelBinder(
                _bagToItemSlotViewModelConverter,
                _bagItemSetFactory.Create(craftingInventoryBehavior),
                _craftingBagItemSlotCollectionViewModel);
            _bagBinder.RequestPopulateHoverCardContent += ItemBinder_RequestPopulateHoverCardContent;

            _itemSetController.Register(_bagBinder);

            _bagBinder.RefreshViewModel();
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

        private void CraftingWindowViewModel_Closed(
            object sender,
            EventArgs e)
        {
            Contract.RequiresNotNull(
                _bagBinder,
                $"Expecting '{nameof(_bagBinder)}' to not be null.");

            _itemSetController.EndPendingDragDrop();
            _itemSetController.Unregister(_bagBinder);

            _bagBinder.RequestPopulateHoverCardContent -= ItemBinder_RequestPopulateHoverCardContent;
            _bagBinder = null;
        }

        private void CraftingWindowViewModel_RequestCraft(
            object sender,
            EventArgs e)
        {
            var filterAttributes = _filterContextProvider
                .GetContext()
                .Attributes
                .ToArray();
            var ingredients = _bagBinder
                .ItemSet
                .Items
                .ToDictionary(x => x.Key, x => x.Value);

            if (_craftingHandler.TryHandle(
                filterAttributes,
                ingredients.Values,
                out var newItems))
            {
                foreach (var itemId in ingredients.Keys)
                {
                    _bagBinder.ItemSet.RemoveItem(itemId);
                }

                foreach (var item in newItems)
                {
                    _bagBinder.ItemSet.AddItem(item);
                }
            }
        }
    }
}