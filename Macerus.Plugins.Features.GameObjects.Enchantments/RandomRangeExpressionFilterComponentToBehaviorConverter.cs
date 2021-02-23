using System;
using System.Collections.Generic;

using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations; // FIXME: dependency on non-API

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class RandomRangeExpressionFilterComponentToBehaviorConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        private readonly IRandom _random;
        private readonly IStatDefinitionToTermConverter _statDefinitionToTermConverter;

        public RandomRangeExpressionFilterComponentToBehaviorConverter(
            IRandom random,
            IStatDefinitionToTermConverter statDefinitionToTermConverter)
        {
            _random = random;
            _statDefinitionToTermConverter = statDefinitionToTermConverter;
        }

        public Type ComponentType { get; } = typeof(RandomRangeExpressionFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent FilterComponent)
        {
            var randomRangeExpressionFilterComponent = (RandomRangeExpressionFilterComponent)FilterComponent;
            var value = _random.NextDouble(
                randomRangeExpressionFilterComponent.MinimumInclusive,
                randomRangeExpressionFilterComponent.MaximumInclusive);
            var term = _statDefinitionToTermConverter[randomRangeExpressionFilterComponent.StatDefinitionId];
            var @operator = randomRangeExpressionFilterComponent.Operator;
            var expression = $"{term}{@operator}{value}";
            yield return new EnchantmentExpressionBehavior(
                randomRangeExpressionFilterComponent.Priority,
                expression);
        }
    }
}
