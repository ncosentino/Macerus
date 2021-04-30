using System.Collections.Generic;
using System.Linq;

using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Api;
using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInteractableBehaviorTests
    {
        private static readonly MacerusContainer _container;
        private static readonly IGameObjectRepositoryAmenity _gameObjectRepositoryAmenity;
        private static readonly IMacerusActorIdentifiers _actorIdentifiers;
        private static readonly IContainerIdentifiers _containerIdentifiers;
        private static readonly IInteractionHandlerFacade _interactionHandler;

        static ContainerInteractableBehaviorTests()
        {
            _container = new MacerusContainer();
            _gameObjectRepositoryAmenity = _container.Resolve<IGameObjectRepositoryAmenity>();
            _actorIdentifiers = _container.Resolve<IMacerusActorIdentifiers>();
            _containerIdentifiers = _container.Resolve<IContainerIdentifiers>();
            _interactionHandler = _container.Resolve<IInteractionHandlerFacade>();
        }

        [Fact]
        public void Interact_ContainerHasDropTableThatTransfers_PlayerGetsItems()
        {
            var container = _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                _containerIdentifiers.ContainerTypeIdentifier,
                new StringIdentifier("some template"),
                new Dictionary<string, object>()
                {
                    ["X"] = 0,
                    ["Y"] = 0,
                    ["Width"] = 1,
                    ["Height"] = 1,
                    ["PrefabId"] = "some prefab",
                    ["DropTableId"] = "any_magic_1-10_lvl10",
                    ["TransferItemsOnActivate"] = true,
                });
            var player = _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                _actorIdentifiers.ActorTypeIdentifier,
                new StringIdentifier("player"),
                new Dictionary<string, object>()
                {
                    ["X"] = 0,
                    ["Y"] = 0,
                    ["Width"] = 1,
                    ["Height"] = 1,
                });

            _interactionHandler.Interact(
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
