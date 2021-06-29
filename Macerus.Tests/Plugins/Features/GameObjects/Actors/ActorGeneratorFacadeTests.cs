using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorGeneratorFacadeTests
    {
        private static readonly MacerusContainer _container;
        private static readonly AssertionHelpers _assertionHelpers;
        private static readonly IActorGeneratorFacade _actorGeneratorFacade;
        private static readonly IFilterContextAmenity _filterContextAmenity;
        private static readonly IGameObjectIdentifiers _gameObjectIdentifiers;
        private static readonly IMacerusActorIdentifiers _actorIdentifiers;
        private static readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;

        static ActorGeneratorFacadeTests()
        {
            _container = new MacerusContainer();
            _assertionHelpers = new AssertionHelpers(_container);
            _actorGeneratorFacade = _container.Resolve<IActorGeneratorFacade>();
            _filterContextAmenity = _container.Resolve<IFilterContextAmenity>();
            _gameObjectIdentifiers = _container.Resolve<IGameObjectIdentifiers>();
            _actorIdentifiers = _container.Resolve<IMacerusActorIdentifiers>();
            _dynamicAnimationIdentifiers = _container.Resolve<IDynamicAnimationIdentifiers>();
        }

        [Fact]
        private void GenerateActors_RequiredRequestPlayer_SingleActor()
        {
            var filterContext = _filterContextAmenity.CreateFilterContextForSingle(
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTypeId,
                    _actorIdentifiers.ActorTypeIdentifier),
                _filterContextAmenity.CreateRequiredAttribute(
                    _actorIdentifiers.ActorDefinitionIdentifier,
                    new StringIdentifier("player")));
            var results = _actorGeneratorFacade
                .GenerateActors(filterContext, Enumerable.Empty<IGeneratorComponent>())
                .ToArray();

            Assert.Single(results);
            var result = results.Single();
            Assert.NotNull(result);
            Assert.Single(result.Get<IPlayerControlledBehavior>());
            var identifierBehavior = Assert.Single(result.Get<IReadOnlyIdentifierBehavior>());
            // FIXME: this is a crutch and we should not have this hardcoded
            // to player. this breaks when we want to do parties with multiple
            // "players" instead of a single dedicated one
            Assert.Equal(new StringIdentifier("player"), identifierBehavior.Id);
            _assertionHelpers.AssertActorRequirements(result);
            _assertionHelpers.AssertStatValue(
                result,
                _dynamicAnimationIdentifiers.AnimationOverrideStatId,
                0);
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
                        _actorIdentifiers.ActorDefinitionIdentifier,
                        new StringIdentifier("player"))),
                3, 
                5);
            var results = _actorGeneratorFacade
                .GenerateActors(filterContext, Enumerable.Empty<IGeneratorComponent>())
                .ToArray();

            Assert.InRange(results.Length, 3, 5);
            foreach (var result in results)
            {
                Assert.Single(result.Get<IPlayerControlledBehavior>());
                _assertionHelpers.AssertActorRequirements(result);
                _assertionHelpers.AssertStatValue(
                    result,
                    _dynamicAnimationIdentifiers.AnimationOverrideStatId,
                    0);
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
                    _actorIdentifiers.ActorDefinitionIdentifier,
                    new StringIdentifier("test-skeleton")),
                _filterContextAmenity.CreateRequiredAttribute(
                    new StringIdentifier("affix-type"),
                    "normal"));
            var results = _actorGeneratorFacade
                .GenerateActors(filterContext, Enumerable.Empty<IGeneratorComponent>())
                .ToArray();

            Assert.Single(results);
            var result = results.Single();
            Assert.NotNull(result);

            Assert.Single(result.Get<IHasStatsBehavior>());
            _assertionHelpers.AssertActorRequirements(result);
            _assertionHelpers.AssertStatValue(
                result,
                _dynamicAnimationIdentifiers.AnimationOverrideStatId,
                1);

            // spawn with items
            var inventoryBehavior = Assert.Single(result
                .Get<IItemContainerBehavior>()
                .Where(x => x
                    .ContainerId
                    .Equals(_actorIdentifiers.InventoryIdentifier)));
            Assert.Equal(3, inventoryBehavior.Items.Count);

            // spawn with equipment
            var equipmentBehavior = Assert.Single(result.Get<ICanEquipBehavior>());
            Assert.InRange(equipmentBehavior.GetEquippedItems().Count(), 0, 3);
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
                        _actorIdentifiers.ActorDefinitionIdentifier,
                        new StringIdentifier("test-skeleton")),
                    _filterContextAmenity.CreateRequiredAttribute(
                        new StringIdentifier("affix-type"),
                        "normal")),
                3,
                5);
            var results = _actorGeneratorFacade
                .GenerateActors(filterContext, Enumerable.Empty<IGeneratorComponent>())
                .ToArray();

            Assert.InRange(results.Length, 3, 5);
            foreach (var result in results)
            {
                Assert.Empty(result.Get<IPlayerControlledBehavior>());
                _assertionHelpers.AssertActorRequirements(result);
                _assertionHelpers.AssertStatValue(
                    result,
                    _dynamicAnimationIdentifiers.AnimationOverrideStatId,
                    1);
            }
        }
    }
}
