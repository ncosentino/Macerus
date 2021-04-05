using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillHandlerFacade : ISkillHandlerFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableSkillHandler> _skillHandlers;

        public SkillHandlerFacade(IEnumerable<IDiscoverableSkillHandler> skillHandlers)
        {
            _skillHandlers = skillHandlers.ToArray();
        }

        public void Handle(
            IGameObject user,
            IGameObject skill,
            IReadOnlyCollection<IGameObject> targets)
        {
            foreach (var skillHandler in _skillHandlers)
            {
                skillHandler.Handle(
                    user,
                    skill,
                    targets);
            }
        }
    }
}
