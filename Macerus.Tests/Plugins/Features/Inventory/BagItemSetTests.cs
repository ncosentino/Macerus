
using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Inventory.Default;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Inventory
{
    public sealed class BagItemSetTests
    {
        private static readonly MacerusContainer _container;
        private static readonly IGameObjectFactory _gameObjectFactory;

        private readonly BagItemSet _bagItemSet;

        static BagItemSetTests()
        {
            _container = new MacerusContainer();
            _gameObjectFactory = _container.Resolve<IGameObjectFactory>();
        }

        public BagItemSetTests()
        {
            _bagItemSet = new BagItemSet(
                new ItemContainerBehavior(new StringIdentifier("inventory")),
                _container.Resolve<ISocketPatternHandlerFacade>(),
                _container.Resolve<ISocketableInfoFactory>());
        }

        [Fact]
        private void SwapItems_NullSwapOutItemSwapIn_SuccessAndContinueTriggersEvent()
        {
            var identifierBehavior = new IdentifierBehavior(new StringIdentifier("the item being swapped"));
            var itemToSwapIn = _gameObjectFactory.Create(new[]
            {
                identifierBehavior,
            });

            var itemsChangedEvent = 0;
            _bagItemSet.ItemsChanged += (s, e) => itemsChangedEvent++;

            var result = _bagItemSet.SwapItems(
                null,
                itemToSwapIn);

            Assert.Equal(SwapResult.SuccessAndContinue, result);
            Assert.Equal(1, itemsChangedEvent);

            var singleEntry = Assert.Single(_bagItemSet.Items);
            Assert.Equal(
                itemToSwapIn,
                singleEntry.Value);
            Assert.Equal(
                identifierBehavior.Id,
                singleEntry.Key);
        }

        [Fact]
        private void SwapItems_ExistingIdSwapOutNoItemSwapIn_SuccessAndContinueTriggersEvent()
        {
            var identifierBehavior = new IdentifierBehavior(new StringIdentifier("the item being swapped"));
            var itemToSwapIn = _gameObjectFactory.Create(new[]
            {
                identifierBehavior,
            });

            // put the item in since we're going to try and remove it
            _bagItemSet.SwapItems(
                null,
                itemToSwapIn);

            var itemsChangedEvent = 0;
            _bagItemSet.ItemsChanged += (s, e) => itemsChangedEvent++;

            var result = _bagItemSet.SwapItems(
                identifierBehavior.Id,
                null);

            Assert.Equal(SwapResult.SuccessAndContinue, result);
            Assert.Equal(1, itemsChangedEvent);
            Assert.Empty(_bagItemSet.Items);
        }

        [Fact]
        private void SwapItems_SameNull_SuccessAndStopNoEvent()
        {
            var itemsChangedEvent = 0;
            _bagItemSet.ItemsChanged += (s, e) => itemsChangedEvent++;

            var result = _bagItemSet.SwapItems(
                null,
                null);

            Assert.Equal(SwapResult.SuccessAndStop, result);
            Assert.Equal(0, itemsChangedEvent);
            Assert.Empty(_bagItemSet.Items);
        }

        [Fact]
        private void SwapItems_SameNotNull_SuccessAndStopNoEvent()
        {
            var identifierBehavior = new IdentifierBehavior(new StringIdentifier("the item being swapped"));
            var itemToSwapIn = _gameObjectFactory.Create(new[]
            {
                identifierBehavior,
            });

            // put the item in since we're going to try and remove it
            _bagItemSet.SwapItems(
                null,
                itemToSwapIn);

            var itemsChangedEvent = 0;
            _bagItemSet.ItemsChanged += (s, e) => itemsChangedEvent++;

            var result = _bagItemSet.SwapItems(
                identifierBehavior.Id,
                itemToSwapIn);

            Assert.Equal(SwapResult.SuccessAndStop, result);
            Assert.Equal(0, itemsChangedEvent);
            var singleEntry = Assert.Single(_bagItemSet.Items);
            Assert.Equal(
                itemToSwapIn,
                singleEntry.Value);
        }

        [Fact]
        private void SwapItems_SameStackIdUnderLimit_SuccessAndStopTargetStackIncreased()
        {
            var existingItemIdBehavior = new IdentifierBehavior(new StringIdentifier("existing item"));
            var existingItem = _gameObjectFactory.Create(new IBehavior[]
            {
                existingItemIdBehavior,
                new StackableItemBehavior(
                    new StringIdentifier("stack id"),
                    int.MaxValue,
                    123),
            });

            var itemToAddIdBehavior = new IdentifierBehavior(new StringIdentifier("item to add to stack"));
            var itemToAdd = _gameObjectFactory.Create(new IBehavior[]
            {
                itemToAddIdBehavior,
                new StackableItemBehavior(
                    new StringIdentifier("stack id"),
                    int.MaxValue,
                    456),
            });

            _bagItemSet.AddItem(existingItem);
            _bagItemSet.AddItem(itemToAdd);

            var itemsChangedEvent = 0;
            _bagItemSet.ItemsChanged += (s, e) => itemsChangedEvent++;

            var result = _bagItemSet.SwapItems(
                existingItemIdBehavior.Id,
                itemToAdd);

            Assert.Equal(SwapResult.SuccessAndStop, result);
            Assert.Equal(1, itemsChangedEvent);
            var singleEntry = Assert.Single(_bagItemSet.Items);
            Assert.Equal(
                existingItemIdBehavior.Id,
                singleEntry.Key);
            Assert.Equal(
                existingItem,
                singleEntry.Value);
            Assert.Equal(
                579,
                existingItem.GetOnly<IStackableItemBehavior>().Count);
            Assert.Equal(
                0,
                itemToAdd.GetOnly<IStackableItemBehavior>().Count);
        }
    }
}
