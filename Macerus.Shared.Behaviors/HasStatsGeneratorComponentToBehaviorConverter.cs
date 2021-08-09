using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Shared.Behaviors
{
    public sealed class HasStatsGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly IHasStatsBehaviorFactory _hasStatsBehaviorFactory;

        public HasStatsGeneratorComponentToBehaviorConverter(IHasStatsBehaviorFactory hasStatsBehaviorFactory)
        {
            _hasStatsBehaviorFactory = hasStatsBehaviorFactory;
        }

        public Type ComponentType => typeof(HasStatsGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var hasStatsGeneratorComponent = (HasStatsGeneratorComponent)generatorComponent;
            var hasStatsBehavior = _hasStatsBehaviorFactory.Create();
            hasStatsBehavior.MutateStats(stats =>
            {
                foreach (var stat in hasStatsGeneratorComponent.Stats)
                {
                    stats[stat.Key] = stat.Value;
                }
            });
            yield return hasStatsBehavior;
        }
    }
}
