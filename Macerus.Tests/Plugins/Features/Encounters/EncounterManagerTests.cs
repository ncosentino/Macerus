using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Encounters;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Encounters
{
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
        }

        [Fact]
        private void StartEncounter_TestEncounter_ExpectedState()
        {
            _testAmenities.UsingCleanMapAndObjects(() =>
            {
                // FIXME: this forces player spawn
                _mapManager.SwitchMap(new StringIdentifier("swamp"));

                var filterContext = _filterContextProvider.GetContext();
                _encounterManager.StartEncounter(
                    filterContext,
                    new StringIdentifier("test-encounter"));

                Assert.True(
                    _combatTurnManager.InCombat,
                    $"Expected '{nameof(_combatTurnManager)}.{nameof(ICombatTurnManager.InCombat)}' to be true.");
                Assert.Equal(new StringIdentifier("test_encounter_map"), _mapManager.ActiveMap.Id);
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
            });
        }

        [Fact]
        private void EndCombat_TestEncounterPlayerWins_TriggerOnCombatEndSpawners()
        {
            _testAmenities.UsingCleanMapAndObjects(() =>
            {
                // FIXME: this forces player spawn
                _mapManager.SwitchMap(new StringIdentifier("swamp"));

                var filterContext = _filterContextProvider.GetContext();
                _encounterManager.StartEncounter(
                    filterContext,
                    new StringIdentifier("test-encounter"));

                Assert.Single(_mapGameObjectManager
                    .GameObjects
                    .Where(x => 
                        x.Has<ITriggerOnCombatEndBehavior>() &&
                        x.Has<IReadOnlySpawnTemplatePropertiesBehavior>()));
                Assert.Single(_mapGameObjectManager
                    .GameObjects
                    .Where(x => x.GetOnly<ITypeIdentifierBehavior>()
                        .TypeId
                        .Equals(new StringIdentifier("container"))));

                _combatTurnManager.EndCombat(
                    Enumerable.Empty<IGameObject>(),
                    new Dictionary<int, IReadOnlyCollection<IGameObject>>());
                _gameEngine.Update();

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
    }
}
