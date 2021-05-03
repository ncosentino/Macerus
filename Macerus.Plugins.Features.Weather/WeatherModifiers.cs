using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api.Handlers;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Weather
{
    public sealed class WeatherModifiers : IWeatherModifiers
    {
        private readonly HashSet<IGameObject> _hasStatsCache;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;
        private readonly IComponentsForTargetComponentFactory _componentsForTargetComponentFactory;
        private readonly ILogger _logger;

        public WeatherModifiers(
            IReadOnlyMapGameObjectManager mapGameObjectManager,
            IStatCalculationService statCalculationService,
            IStatCalculationContextFactory statCalculationContextFactory,
            ILogger logger,
            IComponentsForTargetComponentFactory componentsForTargetComponentFactory)
        {
            _hasStatsCache = new HashSet<IGameObject>();
            _mapGameObjectManager = mapGameObjectManager;
            _mapGameObjectManager.Synchronized += GameObjectManager_Synchronized;
            _statCalculationService = statCalculationService;
            _statCalculationContextFactory = statCalculationContextFactory;
            _logger = logger;
            _componentsForTargetComponentFactory = componentsForTargetComponentFactory;
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
            var statDefinitionId = new StringIdentifier(
                $"{weatherId}-duration-maximum");

            // FIXME: design this so that people can only use multipliers 
            // without direct add/subtract
            var recursiveAccumulator = baseMaximumDuration;
            foreach (var gameObject in _hasStatsCache)
            {
                var context = CreateBaseValueFilterContext(
                    gameObject,
                    statDefinitionId,
                    recursiveAccumulator);
                recursiveAccumulator = _statCalculationService.GetStatValue(
                    gameObject,
                    statDefinitionId,
                    context);
            }

            _logger.Debug(
                $"Maximum duration for '{weatherId}' set to {recursiveAccumulator} (base=" +
                $"{baseMaximumDuration}).");
            return recursiveAccumulator;
        }

        public double GetMinimumDuration(
            IIdentifier weatherId,
            double baseMinimumDuration,
            double maximumDuration)
        {
            IIdentifier minimumStatDefinitionId = new StringIdentifier(
                $"{weatherId}-duration-minimum");
            IIdentifier maximumStatDefinitionId = new StringIdentifier(
                $"{weatherId}-duration-maximum");

            // FIXME: design this so that people can only use multipliers 
            // without direct add/subtract
            var recursiveAccumulator = baseMinimumDuration;
            foreach (var gameObject in _hasStatsCache)
            {
                var context = CreateBaseValuesFilterContext(
                    gameObject,
                    Tuple.Create(minimumStatDefinitionId, recursiveAccumulator),
                    Tuple.Create(maximumStatDefinitionId, maximumDuration));
                recursiveAccumulator = _statCalculationService.GetStatValue(
                    gameObject,
                    minimumStatDefinitionId,
                    context);
            }

            _logger.Debug(
                $"Minimum duration for '{weatherId}' set to {recursiveAccumulator} (base=" +
                $"{baseMinimumDuration}).");
            return recursiveAccumulator;
        }

        public IReadOnlyDictionary<IIdentifier, double> GetWeights(IReadOnlyDictionary<IIdentifier, double> weatherWeights)
        {
            var newWeights = new Dictionary<IIdentifier, double>();

            // FIXME: there's currently no way to tell the back-end that we 
            // want an entirely new entry for a weather ID. Do we want that 
            // flexibility? Can we let players make it rain in places where it 
            // cannot rain?
            foreach (var kvp in weatherWeights)
            {
                var weatherId = kvp.Key;
                var baseWeight = kvp.Value;
                var statDefinitionId = new StringIdentifier(
                    $"{weatherId}-weight");

                // FIXME: design this so that people can only use multipliers 
                // without direct add/subtract
                var recursiveAccumulator = baseWeight;
                foreach (var gameObject in _hasStatsCache)
                {
                    var context = CreateBaseValueFilterContext(
                        gameObject,
                        statDefinitionId,
                        recursiveAccumulator);
                    recursiveAccumulator = _statCalculationService.GetStatValue(
                        gameObject,
                        statDefinitionId,
                        context);
                }

                newWeights[weatherId] = recursiveAccumulator;

                if (recursiveAccumulator != baseWeight)
                {
                    _logger.Debug(
                        $"Weight for '{weatherId}' set to {recursiveAccumulator} (base=" +
                        $"{baseWeight}).");
                }
            }

            return newWeights;
        }

        private IStatCalculationContext CreateBaseValueFilterContext(
            IGameObject target,
            IIdentifier statDefinitionId,
            double value)
        {
            var context = CreateBaseValuesFilterContext(
                target,
                Tuple.Create(statDefinitionId, value));
            return context;
        }

        private IStatCalculationContext CreateBaseValuesFilterContext(
            IGameObject target,
            params Tuple<IIdentifier, double>[] stats)
        {
            var context = _statCalculationContextFactory.Create(
                stats.Select(s => _componentsForTargetComponentFactory.Create(
                    target,
                    new[]
                    {
                        new OverrideBaseStatComponent(
                        s.Item1,
                        s.Item2,
                        int.MinValue),
                    })),
                new IGameObject[] { });
            return context;
        }
    }
}
