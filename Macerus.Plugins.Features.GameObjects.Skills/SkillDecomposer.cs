using Macerus.Plugins.Features.GameObjects.Skills.Api;
using ProjectXyz.Api.GameObjects;

using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillDecomposer : ISkillDecomposer
    {
        public IEnumerable<IGameObject> Decompose(IGameObject skill)
        {
            if (!skill.TryGetFirst<ICombinationSkillBehavior>(out var combinedSkills))
            {
                return new[] { skill };
            }

            return combinedSkills.Skills;
        }
    }
}
