﻿using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors;

namespace Macerus.Shared.Behaviors
{
    public sealed class HasMutableStatsGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly IHasMutableStatsBehaviorFactory _hasMutableStatsBehaviorFactory;

        public HasMutableStatsGeneratorComponentToBehaviorConverter(IHasMutableStatsBehaviorFactory hasMutableStatsBehaviorFactory)
        {
            _hasMutableStatsBehaviorFactory = hasMutableStatsBehaviorFactory;
        }

        public Type ComponentType => typeof(HasMutableStatsGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var hasMutableStatsGeneratorComponent = (HasMutableStatsGeneratorComponent)generatorComponent;
            var hasMutableStatsBehavior = _hasMutableStatsBehaviorFactory.Create();
            hasMutableStatsBehavior.MutateStats(stats =>
            {
                foreach (var stat in hasMutableStatsGeneratorComponent.Stats)
                {
                    stats[stat.Key] = stat.Value;
                }
            });
            yield return hasMutableStatsBehavior;
        }
    }
}