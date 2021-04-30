using Macerus.Plugins.Features.GameObjects.Skills.Api;
using ProjectXyz.Api.GameObjects;

using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillDecomposer : ISkillDecomposer
    {
        private readonly ISkillAmenity _skillAmenity;

        public SkillDecomposer(
            ISkillAmenity skillAmenity)
        {
            _skillAmenity = skillAmenity;
        }

        public IEnumerable<IGameObject> Decompose(IGameObject skill)
        {
            if (!skill.TryGetFirst<ICombinationSkillBehavior>(out var combinedSkills))
            {
                return new[] { skill };
            }

            return combinedSkills
                .SkillIds
                .Select(x => _skillAmenity.GetSkillById(x));
        }
    }
}
