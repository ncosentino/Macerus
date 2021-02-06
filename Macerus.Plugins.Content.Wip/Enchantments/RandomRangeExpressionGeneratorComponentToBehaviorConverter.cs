﻿using NexusLabs.Framework;

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

        public IEnumerable<IBehavior> Convert(IGeneratorComponent generatorComponent)
        {
            var randomRangeExpressionGeneratorComponent = (RandomRangeExpressionGeneratorComponent)generatorComponent;
            var value = _random.NextDouble(
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
