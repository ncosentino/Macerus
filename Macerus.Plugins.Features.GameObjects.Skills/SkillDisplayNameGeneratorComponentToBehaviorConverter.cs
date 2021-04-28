using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillDisplayNameGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(SkillDisplayNameGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var iconGeneratorComponent = (SkillDisplayNameGeneratorComponent)generatorComponent;
            yield return new HasSkillDisplayName(iconGeneratorComponent.DisplayName);
        }
    }
}
