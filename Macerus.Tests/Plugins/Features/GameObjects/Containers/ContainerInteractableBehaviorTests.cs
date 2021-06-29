using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Containers;
using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInteractableBehaviorTests
    {
        private static readonly TestAmenities _testAmenities;
        private static readonly MacerusContainer _container;
        private static readonly IGameObjectRepositoryAmenity _gameObjectRepositoryAmenity;
        private static readonly IMacerusActorIdentifiers _actorIdentifiers;
        private static readonly IInteractionHandlerFacade _interactionHandler;

        static ContainerInteractableBehaviorTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);
            _gameObjectRepositoryAmenity = _container.Resolve<IGameObjectRepositoryAmenity>();
            _actorIdentifiers = _container.Resolve<IMacerusActorIdentifiers>();
            _interactionHandler = _container.Resolve<IInteractionHandlerFacade>();
        }

        [Fact]
        public async Task Interact_ContainerHasDropTableThatTransfers_PlayerGetsItems()
        {
            var container = _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                new StringIdentifier("container/base"),
                new IBehavior[]
                {
                    new ContainerInteractableBehavior(
                        true,
                        true,
                        true),
                    new ContainerGenerateItemsBehavior(
                        new StringIdentifier("any_magic_1-10_lvl10"),
                        false),
                });
            var player = _testAmenities.CreatePlayerInstance();

            await _interactionHandler.InteractAsync(
                player,
                container.GetOnly<IInteractableBehavior>());

            var playerInventory = player
                .Get<IReadOnlyItemContainerBehavior>()
                .SingleOrDefault(x => x.ContainerId.Equals(_actorIdentifiers.InventoryIdentifier));
            Assert.InRange(
                playerInventory.Items.Count,
                1,
                10);
        }
    }
}
