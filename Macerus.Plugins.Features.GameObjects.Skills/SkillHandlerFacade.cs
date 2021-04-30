using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillHandlerFacade : ISkillHandlerFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableSkillHandler> _skillHandlers;
        private readonly ISkillDecomposer _skillDecomposer;

        public SkillHandlerFacade(
            ISkillDecomposer skillDecomposer,
            IEnumerable<IDiscoverableSkillHandler> skillHandlers)
        {
            _skillHandlers = skillHandlers.ToArray();
            _skillDecomposer = skillDecomposer;
        }

        public void Handle(
            IGameObject user,
            IGameObject skill)
        {
            foreach (var decomposedSkill in _skillDecomposer.Decompose(skill))
            {
                foreach (var skillHandler in _skillHandlers)
                {
                    skillHandler.Handle(
                        user,
                        decomposedSkill);
                }
            }
        }
    }
}
