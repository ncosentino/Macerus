using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills;

namespace Macerus.Shared.Behaviors
{
    public sealed class HasSkillsGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly Lazy<ISkillAmenity> _lazySkillAmenity;

        public HasSkillsGeneratorComponentToBehaviorConverter(Lazy<ISkillAmenity> lazySkillAmenity)
        {
            _lazySkillAmenity = lazySkillAmenity;
        }

        public Type ComponentType => typeof(HasSkillsGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var hasSkillsGeneratorComponent = (HasSkillsGeneratorComponent)generatorComponent;

            // FIXME: factor in the levels into this?????
            var skills = hasSkillsGeneratorComponent
                .SkillIdAndLevel
                .Keys
                .Select(x => _lazySkillAmenity.Value.GetSkillById(x));
            var hasSkillsBehavior = new HasSkillsBehavior(skills);
            yield return hasSkillsBehavior;
        }
    }
}
