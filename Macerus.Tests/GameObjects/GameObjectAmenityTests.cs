using System.Collections.Generic;

using Macerus.Api.GameObjects;

using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.GameObjects
{
    public sealed class GameObjectAmenityTests
    {
        private static readonly MacerusContainer _container;
        private static readonly IGameObjectRepositoryAmenity _gameObjectRepositoryAmenity;
        private static readonly IActorIdentifiers _actorIdentifiers;

        static GameObjectAmenityTests()
        {
            _container = new MacerusContainer();
            _gameObjectRepositoryAmenity = _container.Resolve<IGameObjectRepositoryAmenity>();
            _actorIdentifiers = _container.Resolve<IActorIdentifiers>();
        }

        [Fact]
        private void LoadSingleGameObjectFromTemplate_PlayerNoProperties_NotNull()
        {
            var player = _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                _actorIdentifiers.ActorTypeIdentifier,
                new StringIdentifier("player"),
                new Dictionary<string, object>());
            Assert.NotNull(player);
        }
    }
}
