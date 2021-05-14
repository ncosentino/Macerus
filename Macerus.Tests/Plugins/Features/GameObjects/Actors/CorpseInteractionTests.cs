using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
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

        private void Setup(out IGameObject skeleton)
        {
            var filterContext = _filterContextAmenity.CreateNoneFilterContext();
            _encounterManager.StartEncounter(
                filterContext,
                new StringIdentifier("test-encounter"));

            skeleton = _mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x =>
                    !x.Has<IPlayerControlledBehavior>() &&
                    x.GetOnly<ITypeIdentifierBehavior>().TypeId.Equals(_actorIdentifiers.ActorTypeIdentifier));
        }

        [Fact]
        private void Interact_NotDead_NoItemsTransfered()
        {
            _testAmenities.UsingCleanMapAndObjectsWithPlayer(player =>
            {
                Setup(out var skeleton);

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
                _interactionHandler.Interact(player, skeleton.GetOnly<CorpseInteractableBehavior>());

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
        private void Interact_Dead_AllItemsTransfered()
        {
            _testAmenities.UsingCleanMapAndObjectsWithPlayer(player =>
            {
                Setup(out var skeleton);

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

                skeleton
                    .GetOnly<IHasMutableStatsBehavior>()
                    .MutateStats(stats => stats[_combatStatIdentifiers.CurrentLifeStatId] = 0);

                _interactionHandler.Interact(player, skeleton.GetOnly<CorpseInteractableBehavior>());

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
