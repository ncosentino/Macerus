using System;
using System.Collections.Generic;

using Macerus.Api.GameObjects;
using Macerus.Plugins.Content.Weather;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Weather
{
    public sealed class WeatherModifiersFunctionalTests
    {
        private static readonly MacerusContainer _container;
        private static readonly TestAmenities _testAmenities;
        private static readonly IMapGameObjectManager _mapGameObjectManager;
        private static readonly IWeatherModifiers _weatherModifiers;
        private static readonly IGameObjectRepositoryAmenity _gameObjectRepositoryAmenity;
        private static readonly IActorIdentifiers _actorIdentifiers;
        private static readonly ISkillAmenity _skillAmenity;

        static WeatherModifiersFunctionalTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);

            _mapGameObjectManager = _container.Resolve<IMapGameObjectManager>();
            _weatherModifiers = _container.Resolve<IWeatherModifiers>();
            _gameObjectRepositoryAmenity = _container.Resolve<IGameObjectRepositoryAmenity>();
            _actorIdentifiers = _container.Resolve<IActorIdentifiers>();
            _skillAmenity = _container.Resolve<ISkillAmenity>();
        }

        [Fact]
        private void GetWeights_PlayerPassiveRainSkill_ExpectedWeights()
        {
            _testAmenities.UsingCleanMapAndObjects(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });
                
                _mapGameObjectManager.MarkForAddition(player);
                _mapGameObjectManager.Synchronize();

                var inputWeights = new Dictionary<IIdentifier, double>()
                {
                    [WeatherIds.Rain] = 1,
                    [WeatherIds.Clear] = 3,
                };
                var resultWeights = _weatherModifiers.GetWeights(inputWeights);

                Assert.Equal(10, resultWeights[WeatherIds.Rain]);
                Assert.Equal(3, resultWeights[WeatherIds.Clear]);
            });            
        }

        [Fact]
        private void GetWeights_TwoPlayersOnePassiveRain_ExpectedWeights()
        {
            _testAmenities.UsingCleanMapAndObjects(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _mapGameObjectManager.MarkForAddition(
                    player,
                    CreatePlayer());
                _mapGameObjectManager.Synchronize();

                var inputWeights = new Dictionary<IIdentifier, double>()
                {
                    [WeatherIds.Rain] = 1,
                    [WeatherIds.Clear] = 3,
                };
                var resultWeights = _weatherModifiers.GetWeights(inputWeights);

                Assert.Equal(10, resultWeights[WeatherIds.Rain]);
                Assert.Equal(3, resultWeights[WeatherIds.Clear]);
            });
        }

        [Fact]
        private void GetMinimumDuration_PlayerPassiveRainSkill_ExpectedValue()
        {
            _testAmenities.UsingCleanMapAndObjects(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _mapGameObjectManager.MarkForAddition(player);
                _mapGameObjectManager.Synchronize();

                var result = _weatherModifiers.GetMinimumDuration(
                    WeatherIds.Rain,
                    10000,
                    30000);

                Assert.Equal(20000, result);
            });
        }

        [Fact]
        private void GetMinimumDuration_TwoPlayersOnePassiveRainSkill_ExpectedValue()
        {
            _testAmenities.UsingCleanMapAndObjects(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _mapGameObjectManager.MarkForAddition(
                    player,
                    CreatePlayer());
                _mapGameObjectManager.Synchronize();

                var result = _weatherModifiers.GetMinimumDuration(
                    WeatherIds.Rain,
                    10000,
                    30000);

                Assert.Equal(20000, result);
            });
        }

        [Fact]
        private void GetMaximumDuration_PlayerPassiveRainSkill_ExpectedValue()
        {
            _testAmenities.UsingCleanMapAndObjects(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _mapGameObjectManager.MarkForAddition(player);
                _mapGameObjectManager.Synchronize();

                var result = _weatherModifiers.GetMaximumDuration(WeatherIds.Rain, 10000);

                Assert.Equal(25000, result);
            });
        }

        [Fact]
        private void GetMaximumDuration_TwoPlayersOnePassiveRainSkill_ExpectedValue()
        {
            _testAmenities.UsingCleanMapAndObjects(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _mapGameObjectManager.MarkForAddition(
                    player,
                    CreatePlayer());
                _mapGameObjectManager.Synchronize();

                var result = _weatherModifiers.GetMaximumDuration(WeatherIds.Rain, 10000);

                Assert.Equal(25000, result);
            });
        }

        private IGameObject CreatePlayer()
        {
            return _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                _actorIdentifiers.ActorTypeIdentifier,
                new StringIdentifier("player"),
                new Dictionary<string, object>()
                {
                    ["X"] = 0,
                    ["Y"] = 0,
                    ["Width"] = 1,
                    ["Height"] = 1,
                });
        }
    }
}
