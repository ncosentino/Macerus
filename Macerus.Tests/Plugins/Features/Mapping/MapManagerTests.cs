using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.PartyManagement;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Mapping
{
    public sealed class MapManagerTests
    {
        private static readonly MacerusContainer _container;
        private static readonly TestAmenities _testAmenities;
        private static readonly IMapManager _mapManager;
        private static readonly IMapGameObjectManager _mapGameObjectManager;
        private static readonly IGameObjectRepositoryAmenity _gameObjectRepositoryAmenity;
        private static readonly IRosterManager _rosterManager;

        static MapManagerTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);

            _mapManager = _container.Resolve<IMapManager>();
            _mapGameObjectManager = _container.Resolve<IMapGameObjectManager>();
            _gameObjectRepositoryAmenity = _container.Resolve<IGameObjectRepositoryAmenity>();
            _rosterManager = _container.Resolve<IRosterManager>();
        }

        [Fact]
        private async Task SwitchMap_SameMap_NoOpSamePlayer()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async ___ =>
            {
                var uniqueStatId = new StringIdentifier(Guid.NewGuid().ToString());
                var player = CreatePlayerInstance(_container);
                var playerStats = player.GetOnly<IHasMutableStatsBehavior>();
                playerStats.MutateStats(stats => stats.Add(uniqueStatId, 123));

                player.GetOnly<IRosterBehavior>().IsPartyLeader = true;
                _rosterManager.AddToRoster(player);

                await _mapManager.SwitchMapAsync(new StringIdentifier("swamp"));

                int mapChangeCount = 0;
                _mapManager.MapChanged += (_, __) => mapChangeCount++;
                await _mapManager.SwitchMapAsync(new StringIdentifier("swamp"));

                var playerAfter = _mapGameObjectManager
                    .GameObjects
                    .Single(x => x.Has<IPlayerControlledBehavior>());
                playerStats = playerAfter.GetOnly<IHasMutableStatsBehavior>();

                Assert.Equal(player, playerAfter);
                Assert.Equal(123d, playerStats.BaseStats[uniqueStatId]);
                Assert.Equal(0, mapChangeCount);
            });
        }

        [Fact]
        private async Task SwitchMap_IntermediateThenBackToSameMap_PlayerPersistsAcrossMaps()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async _ =>
            {
                var uniqueStatId = new StringIdentifier(Guid.NewGuid().ToString());
                var player = CreatePlayerInstance(_container);
                var playerStats = player.GetOnly<IHasMutableStatsBehavior>();
                playerStats.MutateStats(stats => stats.Add(uniqueStatId, 123));

                player.GetOnly<IRosterBehavior>().IsPartyLeader = true;
                _rosterManager.AddToRoster(player);

                await _mapManager.SwitchMapAsync(new StringIdentifier("swamp"));

                await _mapManager.SwitchMapAsync(new StringIdentifier("test_encounter_map"));
                player = _mapGameObjectManager
                    .GameObjects
                    .Single(x => x.Has<IPlayerControlledBehavior>());
                playerStats = player.GetOnly<IHasMutableStatsBehavior>();

                var playerInRepository = _gameObjectRepositoryAmenity.LoadGameObject(player
                    .GetOnly<IIdentifierBehavior>()
                    .Id);

                Assert.Equal(player, playerInRepository);
                Assert.Equal(123d, playerStats.BaseStats[uniqueStatId]);

                await _mapManager.SwitchMapAsync(new StringIdentifier("swamp"));
                player = _mapGameObjectManager
                    .GameObjects
                    .Single(x => x.Has<IPlayerControlledBehavior>());
                playerStats = player.GetOnly<IHasMutableStatsBehavior>();

                playerInRepository = _gameObjectRepositoryAmenity.LoadGameObject(player
                    .GetOnly<IIdentifierBehavior>()
                    .Id);

                Assert.Equal(player, playerInRepository);
                Assert.Equal(123d, playerStats.BaseStats[uniqueStatId]);
            });
        }

        private IGameObject CreatePlayerInstance(MacerusContainer container)
        {
            var filterContextAmenity = container.Resolve<IFilterContextAmenity>();
            var actorIdentifiers = container.Resolve<IMacerusActorIdentifiers>();
            var gameObjectIdentifiers = container.Resolve<IGameObjectIdentifiers>();
            var actorGeneratorFacade = container.Resolve<IActorGeneratorFacade>();
            var context = filterContextAmenity.CreateFilterContextForSingle(
                filterContextAmenity.CreateRequiredAttribute(
                    gameObjectIdentifiers.FilterContextTypeId,
                    actorIdentifiers.ActorTypeIdentifier),
                filterContextAmenity.CreateRequiredAttribute(
                    actorIdentifiers.ActorDefinitionIdentifier,
                    new StringIdentifier("player")));
            var player = actorGeneratorFacade
                .GenerateActors(
                    context,
                    new IGeneratorComponent[] { })
                .Single();
            return player;
        }
    }
}
