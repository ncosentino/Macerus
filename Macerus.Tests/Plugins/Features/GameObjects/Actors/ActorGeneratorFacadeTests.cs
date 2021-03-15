using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorGeneratorFacadeTests
    {
        private static readonly MacerusContainer _container;
        private static readonly IActorGeneratorFacade _actorGeneratorFacade;
        private static readonly IFilterContextAmenity _filterContextAmenity;
        private static readonly IGameObjectIdentifiers _gameObjectIdentifiers;
        private static readonly IActorIdentifiers _actorIdentifiers;

        static ActorGeneratorFacadeTests()
        {
            _container = new MacerusContainer();
            _actorGeneratorFacade = _container.Resolve<IActorGeneratorFacade>();
            _filterContextAmenity = _container.Resolve<IFilterContextAmenity>();
            _gameObjectIdentifiers = _container.Resolve<IGameObjectIdentifiers>();
            _actorIdentifiers = _container.Resolve<IActorIdentifiers>();
        }

        [Fact]
        private void GenerateActors_RequiredRequestPlayer_SingleActor()
        {
            var filterContext = _filterContextAmenity.CreateFilterContextForSingle(
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTypeId,
                    _actorIdentifiers.ActorTypeIdentifier),
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTemplateId,
                    new StringIdentifier("player")));
            var results = _actorGeneratorFacade
                .GenerateActors(filterContext)
                .ToArray();

            Assert.Single(results);
            var result = results.Single();
            Assert.NotNull(result);
            Assert.Single(result.Get<IPlayerControlledBehavior>());
        }

        [Fact]
        private void GenerateActors_MultipleRequiredRequestPlayer_ExpectedActors()
        {
            var filterContext = _filterContextAmenity.CopyWithRange(
                _filterContextAmenity.CreateFilterContextForSingle(
                    _filterContextAmenity.CreateRequiredAttribute(
                        _gameObjectIdentifiers.FilterContextTypeId,
                        _actorIdentifiers.ActorTypeIdentifier),
                    _filterContextAmenity.CreateRequiredAttribute(
                        _gameObjectIdentifiers.FilterContextTemplateId,
                        new StringIdentifier("player"))),
                3, 
                5);
            var results = _actorGeneratorFacade
                .GenerateActors(filterContext)
                .ToArray();

            Assert.InRange(results.Length, 3, 5);
            foreach (var result in results)
            {
                Assert.Single(result.Get<IPlayerControlledBehavior>());
            }
        }

        [Fact]
        private void GenerateActors_RequiredRequestNormalTestSkeleton_SingleActor()
        {
            var filterContext = _filterContextAmenity.CreateFilterContextForSingle(
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTypeId,
                    _actorIdentifiers.ActorTypeIdentifier),
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTemplateId,
                    new StringIdentifier("test-skeleton")),
                _filterContextAmenity.CreateRequiredAttribute(
                    new StringIdentifier("affix-type"),
                    new StringIdentifier("normal")));
            var results = _actorGeneratorFacade
                .GenerateActors(filterContext)
                .ToArray();

            Assert.Single(results);
            var result = results.Single();
            Assert.NotNull(result);
        }

        [Fact]
        private void GenerateActors_MultipleRequiredRequestNormalTestSkeleton_ExpectedActors()
        {
            var filterContext = _filterContextAmenity.CopyWithRange(
                _filterContextAmenity.CreateFilterContextForSingle(
                    _filterContextAmenity.CreateRequiredAttribute(
                        _gameObjectIdentifiers.FilterContextTypeId,
                        _actorIdentifiers.ActorTypeIdentifier),
                    _filterContextAmenity.CreateRequiredAttribute(
                        _gameObjectIdentifiers.FilterContextTemplateId,
                        new StringIdentifier("test-skeleton")),
                    _filterContextAmenity.CreateRequiredAttribute(
                        new StringIdentifier("affix-type"),
                        new StringIdentifier("normal"))),
                3,
                5);
            var results = _actorGeneratorFacade
                .GenerateActors(filterContext)
                .ToArray();

            Assert.InRange(results.Length, 3, 5);
            foreach (var result in results)
            {
                // FIXME: we need a better assertion here :)
                Assert.Empty(result.Get<IPlayerControlledBehavior>());
            }
        }
    }
}
