using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.Stats;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;
using System;
using System.Collections.Generic;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class RandomRangeExpressionGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IStatDefinitionToTermConverter _statDefinitionToTermConverter;

        public RandomRangeExpressionGeneratorComponentToBehaviorConverter(
            IRandomNumberGenerator randomNumberGenerator,
            IStatDefinitionToTermConverter statDefinitionToTermConverter)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _statDefinitionToTermConverter = statDefinitionToTermConverter;
        }

        public Type ComponentType { get; } = typeof(RandomRangeExpressionGeneratorComponent);

        public IEnumerable<IBehavior> Convert(IGeneratorComponent generatorComponent)
        {
            var randomRangeExpressionGeneratorComponent = (RandomRangeExpressionGeneratorComponent)generatorComponent;
            var value = _randomNumberGenerator.NextInRange(
                randomRangeExpressionGeneratorComponent.MinimumInclusive,
                randomRangeExpressionGeneratorComponent.MaximumInclusive);
            var term = _statDefinitionToTermConverter[randomRangeExpressionGeneratorComponent.StatDefinitionId];
            var @operator = randomRangeExpressionGeneratorComponent.Operator;
            var expression = $"{term}{@operator}{value}";
            yield return new EnchantmentExpressionBehavior(
                randomRangeExpressionGeneratorComponent.Priority,
                expression);
        }
    }
}
