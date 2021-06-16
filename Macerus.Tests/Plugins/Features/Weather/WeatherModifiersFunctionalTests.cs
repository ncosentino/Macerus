using System.Collections.Generic;
using System.Threading.Tasks;

using Macerus.Plugins.Content.Weather;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
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
        private static readonly ISkillAmenity _skillAmenity;

        static WeatherModifiersFunctionalTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);

            _mapGameObjectManager = _container.Resolve<IMapGameObjectManager>();
            _weatherModifiers = _container.Resolve<IWeatherModifiers>();
            _skillAmenity = _container.Resolve<ISkillAmenity>();
        }

        [Fact]
        private async Task GetWeights_PlayerPassiveRainSkill_ExpectedWeights()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var player = _testAmenities.CreatePlayerInstance();
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
        private async Task GetWeights_TwoPlayersOnePassiveRain_ExpectedWeights()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var player = _testAmenities.CreatePlayerInstance();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _mapGameObjectManager.MarkForAddition(
                    player,
                    _testAmenities.CreatePlayerInstance());
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
        private async Task GetMinimumDuration_PlayerPassiveRainSkill_ExpectedValue()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var player = _testAmenities.CreatePlayerInstance();
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
        private async Task GetMinimumDuration_TwoPlayersOnePassiveRainSkill_ExpectedValue()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var player = _testAmenities.CreatePlayerInstance();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _mapGameObjectManager.MarkForAddition(
                    player,
                    _testAmenities.CreatePlayerInstance());
                _mapGameObjectManager.Synchronize();

                var result = _weatherModifiers.GetMinimumDuration(
                    WeatherIds.Rain,
                    10000,
                    30000);

                Assert.Equal(20000, result);
            });
        }

        [Fact]
        private async Task GetMaximumDuration_PlayerPassiveRainSkill_ExpectedValue()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var player = _testAmenities.CreatePlayerInstance();
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
        private async Task GetMaximumDuration_TwoPlayersOnePassiveRainSkill_ExpectedValue()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var player = _testAmenities.CreatePlayerInstance();
                player
                    .GetOnly<IHasSkillsBehavior>()
                    .Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-rain")) });

                _mapGameObjectManager.MarkForAddition(
                    player,
                    _testAmenities.CreatePlayerInstance());
                _mapGameObjectManager.Synchronize();

                var result = _weatherModifiers.GetMaximumDuration(WeatherIds.Rain, 10000);

                Assert.Equal(25000, result);
            });
        }
    }
}
