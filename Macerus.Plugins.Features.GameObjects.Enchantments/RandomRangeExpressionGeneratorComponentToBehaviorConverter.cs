using System;
using System.Collections.Generic;

using NexusLabs.Framework;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Stats;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations; // FIXME: dependency on non-API

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class RandomRangeExpressionGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly IRandom _random;
        private readonly IStatDefinitionToTermConverter _statDefinitionToTermConverter;

        public RandomRangeExpressionGeneratorComponentToBehaviorConverter(
            IRandom random,
            IStatDefinitionToTermConverter statDefinitionToTermConverter)
        {
            _random = random;
            _statDefinitionToTermConverter = statDefinitionToTermConverter;
        }

        public Type ComponentType { get; } = typeof(RandomRangeExpressionGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var randomRangeExpressionGeneratorComponent = (RandomRangeExpressionGeneratorComponent)generatorComponent;
            var value = _random.NextDouble(
                randomRangeExpressionGeneratorComponent.MinimumInclusive,
                randomRangeExpressionGeneratorComponent.MaximumInclusive);
            value = Math.Round(value, randomRangeExpressionGeneratorComponent.DecimalPlaces);
            var term = _statDefinitionToTermConverter[randomRangeExpressionGeneratorComponent.StatDefinitionId];
            var @operator = randomRangeExpressionGeneratorComponent.Operator;
            var expression = $"{term}{@operator}{value}";
            yield return new EnchantmentExpressionBehavior(
                randomRangeExpressionGeneratorComponent.Priority,
                expression);
        }
    }
}
