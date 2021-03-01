using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Weather
{
    public sealed class WeatherModifiers : IWeatherModifiers
    {
        private readonly HashSet<IGameObject> _hasStatsCache;
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;

        public WeatherModifiers(
            IGameObjectManager gameObjectManager,
            IStatCalculationService statCalculationService,
            IStatCalculationContextFactory statCalculationContextFactory)
        {
            _hasStatsCache = new HashSet<IGameObject>();
            _gameObjectManager = gameObjectManager;
            _gameObjectManager.Synchronized += GameObjectManager_Synchronized;
            _statCalculationService = statCalculationService;
            _statCalculationContextFactory = statCalculationContextFactory;
        }

        private void GameObjectManager_Synchronized(
            object sender,
            GameObjectsSynchronizedEventArgs e)
        {
            foreach (var removedObject in e.Removed)
            {
                _hasStatsCache.Remove(removedObject);
            }

            foreach (var addedObject in e.Added.Where(x => x.Has<IHasStatsBehavior>()))
            {
                _hasStatsCache.Add(addedObject);
            }
        }

        public double GetMaximumDuration(IIdentifier weatherId, double baseMaximumDuration)
        {
            // FIXME: how can we pass in the base maximum duration into the 
            // calculation for things like x% longer rain
            var context = _statCalculationContextFactory.Create(
                new IComponent[] { },
                new IEnchantment[] { });
            var statDefinitionId = new StringIdentifier(
                $"{weatherId}-duration-maximum");

            var accumulator = 0d;
            foreach (var gameObject in _hasStatsCache)
            {
                accumulator += _statCalculationService.GetStatValue(
                    gameObject,
                    statDefinitionId,
                    context);
            }

            return baseMaximumDuration + accumulator;
        }

        public double GetMinimumDuration(IIdentifier weatherId, double baseMinimumDuration)
        {
            // FIXME: how can we pass in the base minimum duration into the 
            // calculation for things like x% longer rain
            var context = _statCalculationContextFactory.Create(
                new IComponent[] { },
                new IEnchantment[] { });
            var statDefinitionId = new StringIdentifier(
                $"{weatherId}-duration-minimum");

            var accumulator = 0d;
            foreach (var gameObject in _hasStatsCache)
            {
                accumulator += _statCalculationService.GetStatValue(
                    gameObject,
                    statDefinitionId,
                    context);
            }

            return baseMinimumDuration + accumulator;
        }

        public IReadOnlyDictionary<IIdentifier, double> GetWeights(IReadOnlyDictionary<IIdentifier, double> weatherWeights)
        {
            // FIXME: use existing game objects to calculate their effect on this
            return weatherWeights;
        }
    }
}
