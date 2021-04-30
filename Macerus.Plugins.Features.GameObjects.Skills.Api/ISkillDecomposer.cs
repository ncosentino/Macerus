using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ISkillDecomposer
    {
        IEnumerable<IGameObject> Decompose(IGameObject skill);
    }
}
