using System.Linq;

using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Inventory.Default;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Inventory
{
    public sealed class ItemSetControllerTests
    {
        private static readonly MacerusContainer _container;
        private static readonly IGameObjectFactory _gameObjectFactory;

        private readonly ItemSetController _itemSetController;
        private readonly ItemDragViewModel _itemDragViewModel;

        static ItemSetControllerTests()
        {
            _container = new MacerusContainer();
            _gameObjectFactory = _container.Resolve<IGameObjectFactory>();
        }

        public ItemSetControllerTests()
        {
            _itemDragViewModel = new ItemDragViewModel();
            _itemSetController = new ItemSetController(_itemDragViewModel);
        }

        [Fact]
        private void DragDrop_ItemStacksFromSameBagUnderLimit_ConsolidatesStacks()
        {
            var draggedItemStackIdBehavior = new IdentifierBehavior(new StringIdentifier("dragged item stack"));
            var draggedItemStack = _gameObjectFactory.Create(new IBehavior[]
            {
                draggedItemStackIdBehavior,
                new StackableItemBehavior(
                    new StringIdentifier("stack id"),
                    int.MaxValue,
                    123),
            });

            var destinationItemStackIdBehavior = new IdentifierBehavior(new StringIdentifier("destination item stack"));
            var destinationItemStack = _gameObjectFactory.Create(new IBehavior[]
            {
                destinationItemStackIdBehavior,
                new StackableItemBehavior(
                    new StringIdentifier("stack id"),
                    int.MaxValue,
                    456),
            });

            var itemContainerBehavior = new ItemContainerBehavior(new StringIdentifier("inventory"));
            itemContainerBehavior.TryAddItem(draggedItemStack);
            itemContainerBehavior.TryAddItem(destinationItemStack);

            var bagBinder = CreateBagBinder(itemContainerBehavior);
            _itemSetController.Register(bagBinder);

            bagBinder
                .ItemSlotCollectionViewModel
                .StartDragItem(bagBinder
                    .ItemSlotCollectionViewModel
                    .ItemSlots
                    .First(x => x.Id.Equals("dragged item stack")));
            bagBinder
                .ItemSlotCollectionViewModel
                .DropItem(bagBinder
                    .ItemSlotCollectionViewModel
                    .ItemSlots
                    .First(x => x.Id.Equals("destination item stack")));

            var singleViewModel = Assert.Single(bagBinder.ItemSlotCollectionViewModel.ItemSlots);
            Assert.Equal("destination item stack", singleViewModel.Id);
            Assert.Equal(579, destinationItemStack.GetOnly<IStackableItemBehavior>().Count);
            Assert.Equal(0, draggedItemStack.GetOnly<IStackableItemBehavior>().Count);
            Assert.DoesNotContain(draggedItemStack, itemContainerBehavior.Items);
            Assert.Contains(destinationItemStack, itemContainerBehavior.Items);
        }

        [Fact]
        private void DragDrop_ItemStacksFromSameBagOverLimit_DoesNotConsolidateStacks()
        {
            var draggedItemStackIdBehavior = new IdentifierBehavior(new StringIdentifier("dragged item stack"));
            var draggedItemStack = _gameObjectFactory.Create(new IBehavior[]
            {
                draggedItemStackIdBehavior,
                new StackableItemBehavior(
                    new StringIdentifier("stack id"),
                    int.MaxValue,
                    123),
            });

            var destinationItemStackIdBehavior = new IdentifierBehavior(new StringIdentifier("destination item stack"));
            var destinationItemStack = _gameObjectFactory.Create(new IBehavior[]
            {
                destinationItemStackIdBehavior,
                new StackableItemBehavior(
                    new StringIdentifier("stack id"),
                    500,
                    456),
            });

            var itemContainerBehavior = new ItemContainerBehavior(new StringIdentifier("inventory"));
            itemContainerBehavior.TryAddItem(draggedItemStack);
            itemContainerBehavior.TryAddItem(destinationItemStack);

            var bagBinder = CreateBagBinder(itemContainerBehavior);
            _itemSetController.Register(bagBinder);

            bagBinder
                .ItemSlotCollectionViewModel
                .StartDragItem(bagBinder
                    .ItemSlotCollectionViewModel
                    .ItemSlots
                    .First(x => x.Id.Equals("dragged item stack")));
            bagBinder
                .ItemSlotCollectionViewModel
                .DropItem(bagBinder
                    .ItemSlotCollectionViewModel
                    .ItemSlots
                    .First(x => x.Id.Equals("destination item stack")));

            Assert.Equal(2, bagBinder.ItemSlotCollectionViewModel.ItemSlots.Count());
            Assert.Equal(500, destinationItemStack.GetOnly<IStackableItemBehavior>().Count);
            Assert.Equal(79, draggedItemStack.GetOnly<IStackableItemBehavior>().Count);
            Assert.Contains(draggedItemStack, itemContainerBehavior.Items);
            Assert.Contains(destinationItemStack, itemContainerBehavior.Items);
        }

        private IItemSetToViewModelBinder CreateBagBinder(IItemContainerBehavior itemContainerBehavior)
        {
            var bagItemSet = new BagItemSet(
                itemContainerBehavior,
                _container.Resolve<IInventorySocketingWorkflow>());
            var binder = new ItemSetToViewModelBinder(
                new BagItemToItemSlotViewModelConverter(),
                bagItemSet,
                new ItemSlotCollectionViewModel());
            binder.RefreshViewModel();
            return binder;
        }
    }
}
