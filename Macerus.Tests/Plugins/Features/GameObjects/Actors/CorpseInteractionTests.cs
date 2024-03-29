﻿using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Actors.Default;
using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Actors
{
    public sealed class CorpseInteractionTests
    {
        private static readonly MacerusContainer _container;
        private static readonly AssertionHelpers _assertionHelpers;
        private static readonly TestAmenities _testAmenities;

        private static readonly IMapGameObjectManager _mapGameObjectManager;
        private static readonly IFilterContextAmenity _filterContextAmenity;
        private static readonly ICombatStatIdentifiers _combatStatIdentifiers;
        private static readonly IEncounterManager _encounterManager;
        private static readonly IMacerusActorIdentifiers _actorIdentifiers;
        private static readonly IInteractionHandlerFacade _interactionHandler;

        static CorpseInteractionTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);
            _assertionHelpers = new AssertionHelpers(_container);

            _mapGameObjectManager = _container.Resolve<IMapGameObjectManager>();
            _filterContextAmenity = _container.Resolve<IFilterContextAmenity>();
            _combatStatIdentifiers = _container.Resolve<ICombatStatIdentifiers>();
            _encounterManager = _container.Resolve<IEncounterManager>();
            _actorIdentifiers = _container.Resolve<IMacerusActorIdentifiers>();
            _interactionHandler = _container.Resolve<IInteractionHandlerFacade>();
        }

        private async Task<IGameObject> SetupAsync()
        {
            var filterContext = _filterContextAmenity.CreateNoneFilterContext();
            await _encounterManager.StartEncounterAsync(
                filterContext,
                new StringIdentifier("test-encounter"));

            var skeleton = _mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x =>
                    !x.Has<IPlayerControlledBehavior>() &&
                    x.GetOnly<ITypeIdentifierBehavior>().TypeId.Equals(_actorIdentifiers.ActorTypeIdentifier));
            return skeleton;
        }

        [Fact]
        private async Task Interact_NotDead_NoItemsTransfered()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async player =>
            {
                var skeleton = await SetupAsync();

                var skeletonInventory = skeleton
                    .Get<IItemContainerBehavior>()
                    .Single(x => x
                        .ContainerId
                        .Equals(_actorIdentifiers.InventoryIdentifier));
                int expectedSkeletonInventoryItemCount = skeletonInventory.Items.Count;
                int expectedSkeletonEquipmentCount = skeleton
                    .GetOnly<ICanEquipBehavior>()
                    .GetEquippedItems()
                    .Count();

                // should be no-op
                await _interactionHandler.InteractAsync(
                    _filterContextAmenity.GetContext(),
                    player,
                    skeleton.GetOnly<CorpseInteractableBehavior>());

                var playerInventory = player
                    .Get<IItemContainerBehavior>()
                    .Single(x => x
                        .ContainerId
                        .Equals(_actorIdentifiers.InventoryIdentifier));
                Assert.Empty(playerInventory.Items);

                Assert.Equal(
                    expectedSkeletonInventoryItemCount,
                    skeletonInventory.Items.Count);
                Assert.Equal(
                    expectedSkeletonEquipmentCount,
                    skeleton
                        .GetOnly<ICanEquipBehavior>()
                        .GetEquippedItems()
                        .Count());
            });
        }

        [Fact]
        private async Task Interact_Dead_AllItemsTransfered()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async player =>
            {
                var skeleton = await SetupAsync();

                var skeletonInventory = skeleton
                    .Get<IItemContainerBehavior>()
                    .Single(x => x
                        .ContainerId
                        .Equals(_actorIdentifiers.InventoryIdentifier));
                int expectedSkeletonInventoryItemCount = skeletonInventory.Items.Count;
                int expectedSkeletonEquipmentCount = skeleton
                    .GetOnly<ICanEquipBehavior>()
                    .GetEquippedItems()
                    .Count();

                await skeleton
                    .GetOnly<IHasStatsBehavior>()
                    .MutateStatsAsync(async stats => stats[_combatStatIdentifiers.CurrentLifeStatId] = 0)
                    .ConfigureAwait(false);

                await _interactionHandler.InteractAsync(
                    _filterContextAmenity.GetContext(),
                    player,
                    skeleton.GetOnly<CorpseInteractableBehavior>());

                var playerInventory = player
                    .Get<IItemContainerBehavior>()
                    .Single(x => x
                        .ContainerId
                        .Equals(_actorIdentifiers.InventoryIdentifier));
                Assert.Equal(
                    expectedSkeletonEquipmentCount + expectedSkeletonInventoryItemCount,
                    playerInventory.Items.Count);

                Assert.Empty(skeletonInventory.Items);
                Assert.Empty(skeleton
                    .GetOnly<ICanEquipBehavior>()
                    .GetEquippedItems());
            });
        }
    }
}
