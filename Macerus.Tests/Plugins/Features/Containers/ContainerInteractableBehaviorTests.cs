using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Containers;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Containers
{
    public sealed class ContainerInteractableBehaviorTests
    {
        private static readonly MacerusContainer _container;
        private static readonly IGameObjectRepositoryFacade _gameObjectRepository;

        static ContainerInteractableBehaviorTests()
        {
            _container = new MacerusContainer();
            _gameObjectRepository = _container.Resolve<IGameObjectRepositoryFacade>();
        }

        [Fact]
        public void Interact_ContainerHasDropTableThatTransfers_PlayerGetsItems()
        {
            var container = _gameObjectRepository.CreateFromTemplate(
                ContainerRepository.ContainerTypeId,
                new StringIdentifier("some template"),
                new Dictionary<string, object>()
                {
                    ["X"] = 0,
                    ["Y"] = 0,
                    ["Width"] = 1,
                    ["Height"] = 1,
                    ["DropTableId"] = "any_magic_1-10_lvl10",
                    ["TransferItemsOnActivate"] = true,
                });
            var player = _gameObjectRepository.CreateFromTemplate(
                ActorRepository.ActorTypeId,
                new StringIdentifier("player"),
                new Dictionary<string, object>()
                {
                    ["X"] = 0,
                    ["Y"] = 0,
                    ["Width"] = 1,
                    ["Height"] = 1,
                });

            container
                .GetOnly<IInteractableBehavior>()
                .Interact(player);

            var playerInventory = player
                .Get<IReadOnlyItemContainerBehavior>()
                .SingleOrDefault(x => x.ContainerId.Equals(new StringIdentifier("Inventory")));
            Assert.InRange(
                playerInventory.Items.Count,
                1,
                10);
        }
    }
}
