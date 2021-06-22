using System;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Api.GameObjects;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;
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

        static MapManagerTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);

            _mapManager = _container.Resolve<IMapManager>();
            _mapGameObjectManager = _container.Resolve<IMapGameObjectManager>();
            _gameObjectRepositoryAmenity = _container.Resolve<IGameObjectRepositoryAmenity>();
        }

        [Fact]
        private void SwitchMap_SameMap_NoOpSamePlayer()
        {
            _testAmenities.UsingCleanMapAndObjectsWithPlayer(async ___ =>
            {
                await _mapManager.SwitchMapAsync(new StringIdentifier("swamp"));

                var uniqueStatId = new StringIdentifier(Guid.NewGuid().ToString());
                var player = _mapGameObjectManager
                    .GameObjects
                    .Single(x => x.Has<IPlayerControlledBehavior>());
                var playerStats = player.GetOnly<IHasMutableStatsBehavior>();
                playerStats.MutateStats(stats => stats.Add(uniqueStatId, 123));

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
        private void SwitchMap_IntermediateThenBackToSameMap_PlayerPersistsAcrossMaps()
        {
            _testAmenities.UsingCleanMapAndObjectsWithPlayer(async _ =>
            {
                await _mapManager.SwitchMapAsync(new StringIdentifier("swamp"));

                var uniqueStatId = new StringIdentifier(Guid.NewGuid().ToString());
                var player = _mapGameObjectManager
                    .GameObjects
                    .Single(x => x.Has<IPlayerControlledBehavior>());
                var playerStats = player.GetOnly<IHasMutableStatsBehavior>();
                playerStats.MutateStats(stats => stats.Add(uniqueStatId, 123));

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
    }
}
