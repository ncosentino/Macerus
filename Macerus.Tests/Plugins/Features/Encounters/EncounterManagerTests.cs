using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.GameObjects.Static.Doors;
using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.PartyManagement;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Encounters
{
    [CollectionDefinition(nameof(EncounterManagerTests), DisableParallelization = true)]
    public sealed class EncounterManagerTests
    {
        private static readonly MacerusContainer _container;
        private static readonly TestAmenities _testAmenities;
        private static readonly IEncounterManager _encounterManager;
        private static readonly IFilterContextProvider _filterContextProvider;
        private static readonly ICombatTurnManager _combatTurnManager;
        private static readonly IMapGameObjectManager _mapGameObjectManager;
        private static readonly IMapManager _mapManager;
        private static readonly IActorIdentifiers _actorIdentifiers;
        private static readonly IGameEngine _gameEngine;
        private static readonly IInteractionHandler _interactionHandler;
        private static readonly IRosterManager _rosterManager;
        private static readonly ITestModalContentPresenter _testModalContentPresenter;

        static EncounterManagerTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);

            _encounterManager = _container.Resolve<IEncounterManager>();
            _filterContextProvider = _container.Resolve<IFilterContextProvider>();
            _combatTurnManager = _container.Resolve<ICombatTurnManager>();
            _mapGameObjectManager = _container.Resolve<IMapGameObjectManager>();
            _mapManager = _container.Resolve<IMapManager>();
            _actorIdentifiers = _container.Resolve<IActorIdentifiers>();
            _gameEngine = _container.Resolve<IGameEngine>(); // NOTE: we need this to resolve systems.
            _interactionHandler = _container.Resolve<IInteractionHandlerFacade>();
            _rosterManager = _container.Resolve<IRosterManager>();
            _testModalContentPresenter = _container.Resolve<ITestModalContentPresenter>();
        }

        [Fact]
        private async Task StartEncounterAsync_TestEncounter_ExpectedState()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async actor =>
            {
                try
                {
                    _rosterManager.AddToRoster(actor);
                    actor.GetOnly<IRosterBehavior>().IsPartyLeader = true;

                    var filterContext = _filterContextProvider.GetContext();
                    await _encounterManager.StartEncounterAsync(
                        filterContext,
                        new StringIdentifier("test-encounter"));

                    Assert.True(
                        _combatTurnManager.InCombat,
                        $"Expected '{nameof(_combatTurnManager)}.{nameof(ICombatTurnManager.InCombat)}' to be true.");
                    Assert.Equal(new StringIdentifier("test_encounter_map"), _mapManager.ActiveMap.GetOnly<IIdentifierBehavior>().Id);
                    Assert.Single(_mapGameObjectManager.GameObjects.Where(x => x.Has<IPlayerControlledBehavior>()));
                    Assert.InRange(
                        _mapGameObjectManager
                            .GameObjects
                            .Count(x => x
                                .GetOnly<ITypeIdentifierBehavior>()
                                .TypeId
                                .Equals(_actorIdentifiers.ActorTypeIdentifier)),
                        2,
                        10);
                }
                finally
                {
                    _testModalContentPresenter.SetCallback(async buttons => buttons.Single().ButtonSelected());
                    await _combatTurnManager.EndCombatAsync(
                        Enumerable.Empty<IGameObject>(),
                        new Dictionary<int, IReadOnlyCollection<IGameObject>>());
                }
            });
        }

        [Fact]
        private async Task EndCombatAsync_TestEncounterPlayerWins_TriggerOnCombatEndSpawners()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async actor =>
            {
                _testModalContentPresenter.SetCallback(async buttons => buttons.Single().ButtonSelected());

                _rosterManager.AddToRoster(actor);
                actor.GetOnly<IRosterBehavior>().IsPartyLeader = true;

                var filterContext = _filterContextProvider.GetContext();
                await _encounterManager.StartEncounterAsync(
                    filterContext,
                    new StringIdentifier("test-encounter"));

                Assert.Equal(
                    2,
                    _mapGameObjectManager
                        .GameObjects
                        .Where(x => 
                            x.Has<ITriggerOnCombatEndBehavior>() &&
                            x.Has<IReadOnlySpawnTemplatePropertiesBehavior>())
                        .Count());
                Assert.Single(_mapGameObjectManager
                    .GameObjects
                    .Where(x => x.GetOnly<ITypeIdentifierBehavior>()
                        .TypeId
                        .Equals(new StringIdentifier("container"))));

                await _combatTurnManager.EndCombatAsync(
                    Enumerable.Empty<IGameObject>(),
                    new Dictionary<int, IReadOnlyCollection<IGameObject>>());
                await _gameEngine.UpdateAsync();

                Assert.Empty(_mapGameObjectManager
                    .GameObjects
                    .Where(x =>
                        x.Has<ITriggerOnCombatEndBehavior>() &&
                        x.Has<IReadOnlySpawnTemplatePropertiesBehavior>()));
                Assert.Equal(
                    2,
                    _mapGameObjectManager
                        .GameObjects
                        .Count(x => x.GetOnly<ITypeIdentifierBehavior>()
                            .TypeId
                            .Equals(new StringIdentifier("container"))));
            });
        }

        [Fact]
        private async Task EndCombatAsync_UseReturnDoor_BackToStartMapAtSpecifiedLocation()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async actor =>
            {
                _testModalContentPresenter.SetCallback(async buttons => buttons.Single().ButtonSelected());

                _rosterManager.AddToRoster(actor);
                actor.GetOnly<IRosterBehavior>().IsPartyLeader = true;

                var filterContext = _filterContextProvider.GetContext();
                await _encounterManager.StartEncounterAsync(
                    filterContext,
                    new StringIdentifier("test-encounter"));

                await _combatTurnManager.EndCombatAsync(
                    Enumerable.Empty<IGameObject>(),
                    new Dictionary<int, IReadOnlyCollection<IGameObject>>());
                await _gameEngine.UpdateAsync();

                var door = _mapGameObjectManager
                    .GameObjects
                    .Single(x => x.Has<DoorInteractableBehavior>())
                    .GetOnly<DoorInteractableBehavior>();
                await _interactionHandler.InteractAsync(
                    actor,
                    door);

                Assert.Equal(door.TransitionToMapId, _mapManager.ActiveMap.GetOnly<IIdentifierBehavior>().Id);

                var playerPosition = actor.GetOnly<IReadOnlyPositionBehavior>();
                Assert.Equal(door.TransitionToX.Value, playerPosition.X);
                Assert.Equal(door.TransitionToY.Value, playerPosition.Y);
            });
        }
    }
}
