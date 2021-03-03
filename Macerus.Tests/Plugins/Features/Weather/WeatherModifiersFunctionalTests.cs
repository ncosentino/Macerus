using System;
using System.Collections.Generic;

using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Weather;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Weather
{
    public sealed class WeatherModifiersFunctionalTests
    {
        private static readonly MacerusContainer _container;
        private static readonly IMutableGameObjectManager _gameObjectManager;
        private static readonly IWeatherModifiers _weatherModifiers;
        private static readonly IGameObjectRepositoryFacade _gameObjectRepositoryFacade;
        private static readonly IActorIdentifiers _actorIdentifiers;
        private static readonly ISkillAmenity _skillAmenity;

        static WeatherModifiersFunctionalTests()
        {
            _container = new MacerusContainer();

            _gameObjectManager = _container.Resolve<IMutableGameObjectManager>();
            _weatherModifiers = _container.Resolve<IWeatherModifiers>();
            _gameObjectRepositoryFacade = _container.Resolve<IGameObjectRepositoryFacade>();
            _actorIdentifiers = _container.Resolve<IActorIdentifiers>();
            _skillAmenity = _container.Resolve<ISkillAmenity>();
        }

        [Fact]
        private void GetWeights_PlayerPassiveRainSkill_ExpectedWeights()
        {
            UsingCleanObjectManager(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });
                
                _gameObjectManager.MarkForAddition(player);
                _gameObjectManager.Synchronize();

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
            UsingCleanObjectManager(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _gameObjectManager.MarkForAddition(
                    player,
                    CreatePlayer());
                _gameObjectManager.Synchronize();

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
            UsingCleanObjectManager(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _gameObjectManager.MarkForAddition(player);
                _gameObjectManager.Synchronize();

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
            UsingCleanObjectManager(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _gameObjectManager.MarkForAddition(
                    player,
                    CreatePlayer());
                _gameObjectManager.Synchronize();

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
            UsingCleanObjectManager(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _gameObjectManager.MarkForAddition(player);
                _gameObjectManager.Synchronize();

                var result = _weatherModifiers.GetMaximumDuration(WeatherIds.Rain, 10000);

                Assert.Equal(25000, result);
            });
        }

        [Fact]
        private void GetMaximumDuration_TwoPlayersOnePassiveRainSkill_ExpectedValue()
        {
            UsingCleanObjectManager(() =>
            {
                var player = CreatePlayer();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _gameObjectManager.MarkForAddition(
                    player,
                    CreatePlayer());
                _gameObjectManager.Synchronize();

                var result = _weatherModifiers.GetMaximumDuration(WeatherIds.Rain, 10000);

                Assert.Equal(25000, result);
            });
        }

        private IGameObject CreatePlayer()
        {
            return _gameObjectRepositoryFacade.CreateFromTemplate(
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

        private void UsingCleanObjectManager(Action callback)
        {
            _gameObjectManager.MarkForRemoval(_gameObjectManager.GameObjects);
            _gameObjectManager.Synchronize();
            try
            {
                callback.Invoke();
            }
            finally
            {
                _gameObjectManager.MarkForRemoval(_gameObjectManager.GameObjects);
                _gameObjectManager.Synchronize();
            }
        }
    }
}
