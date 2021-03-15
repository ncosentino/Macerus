using System.Collections.Generic;

using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Actors
{
    public sealed class GameObjectRepositoryFacadeTests
    {
        private static readonly MacerusContainer _container;
        private static readonly IGameObjectRepositoryFacade _gameObjectRepositoryFacade;
        private static readonly IFilterContextAmenity _filterContextAmenity;
        private static readonly IGameObjectIdentifiers _gameObjectIdentifiers;
        private static readonly IActorIdentifiers _actorIdentifiers;

        static GameObjectRepositoryFacadeTests()
        {
            _container = new MacerusContainer();
            _gameObjectRepositoryFacade = _container.Resolve<IGameObjectRepositoryFacade>();
            _filterContextAmenity = _container.Resolve<IFilterContextAmenity>();
            _gameObjectIdentifiers = _container.Resolve<IGameObjectIdentifiers>();
            _actorIdentifiers = _container.Resolve<IActorIdentifiers>();
        }

        [Fact]
        private void CreateFromTemplate_RequiredRequestPlayer_SingleActor()
        {
            var filterContext = _filterContextAmenity.CreateFilterContextForSingle(
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTypeId,
                    _actorIdentifiers.ActorTypeIdentifier),
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTemplateId,
                    new StringIdentifier("player")));
            var result = _gameObjectRepositoryFacade.CreateFromTemplate(
                filterContext,
                new Dictionary<string, object>());

            Assert.NotNull(result);
            Assert.Single(result.Get<IPlayerControlledBehavior>());
        }
    }
}
