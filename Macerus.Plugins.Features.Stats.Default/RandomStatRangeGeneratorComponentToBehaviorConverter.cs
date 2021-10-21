using System;
using System.Collections.Generic;

using NexusLabs.Framework;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Stats.Default
{
    public sealed class RandomStatRangeGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public readonly IRandom _random;

        public RandomStatRangeGeneratorComponentToBehaviorConverter(IRandom random)
        {
            _random = random;
        }

        public Type ComponentType => typeof(RandomStatRangeGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext, 
            IEnumerable<IBehavior> baseBehaviors, 
            IGeneratorComponent generatorComponent)
        {
            var randomStatRangeGeneratorComponent = (RandomStatRangeGeneratorComponent)generatorComponent;
            var hasStatsBehavior = baseBehaviors.GetOnly<IHasStatsBehavior>();

            var value = _random.NextDouble(randomStatRangeGeneratorComponent.Minimum, randomStatRangeGeneratorComponent.Maximum);
            value = Math.Round(value, randomStatRangeGeneratorComponent.Decimals);

            hasStatsBehavior
                .MutateStatsAsync(async stats => stats[randomStatRangeGeneratorComponent.StatDefinitionId] = value)
                .Wait();
            yield break;
        }
    }
}
