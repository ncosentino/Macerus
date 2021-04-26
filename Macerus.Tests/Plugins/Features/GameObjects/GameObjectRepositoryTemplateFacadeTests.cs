using System;
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
    public sealed class GameObjectRepositoryTemplateFacadeTests
    {
        private static readonly MacerusContainer _container;
        private static readonly IGameObjectTemplateRepositoryFacade _gameObjectTemplateRepositoryFacade;
        private static readonly IFilterContextAmenity _filterContextAmenity;
        private static readonly IGameObjectIdentifiers _gameObjectIdentifiers;
        private static readonly IActorIdentifiers _actorIdentifiers;

        static GameObjectRepositoryTemplateFacadeTests()
        {
            _container = new MacerusContainer();
            _gameObjectTemplateRepositoryFacade = _container.Resolve<IGameObjectTemplateRepositoryFacade>();
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
            var result = _gameObjectTemplateRepositoryFacade.CreateFromTemplate(
                filterContext,
                new Dictionary<string, object>());

            Assert.NotNull(result);
            Assert.Single(result.Get<IPlayerControlledBehavior>());
        }

        [Fact]
        private void CreateFromTemplate_RequiredRequestNormalTestSkeleton_SingleActor()
        {
            var filterContext = _filterContextAmenity.CreateFilterContextForSingle(
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTypeId,
                    _actorIdentifiers.ActorTypeIdentifier),
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTemplateId,
                    new StringIdentifier("test-skeleton")),
                _filterContextAmenity.CreateSupportedAttribute(
                    new StringIdentifier("affix-type"),
                    "normal"));
            var result = _gameObjectTemplateRepositoryFacade.CreateFromTemplate(
                filterContext,
                new Dictionary<string, object>());

            Assert.NotNull(result);
            Assert.Empty(result.Get<IPlayerControlledBehavior>());
        }

        [Fact]
        private void CreateFromTemplate_RequiredRequestUnsupportedTestSkeleton_Throws()
        {
            var filterContext = _filterContextAmenity.CreateFilterContextForSingle(
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTypeId,
                    _actorIdentifiers.ActorTypeIdentifier),
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTemplateId,
                    new StringIdentifier("test-skeleton")),
                _filterContextAmenity.CreateSupportedAttribute(
                    new StringIdentifier("affix-type"),
                    new StringIdentifier("not a valid affix type")));
            Assert.Throws<InvalidOperationException>(() =>_gameObjectTemplateRepositoryFacade.CreateFromTemplate(
                filterContext,
                new Dictionary<string, object>()));
        }
    }
}
