using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ISkillHandler
    {
        void Handle(
            IGameObject user,
            IGameObject skill,
            IReadOnlyCollection<IGameObject> targets);
    }
}
