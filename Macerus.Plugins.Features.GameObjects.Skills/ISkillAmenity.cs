using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public interface ISkillAmenity
    {
        IGameObject GetSkillById(IIdentifier skillDefinitionId);

        IEnumerable<IGameObject> GetAllSkillEffects(IGameObject skill);

        bool TryGetSkillById(
            IIdentifier skillDefinitionId,
            out IGameObject skill);

        bool IsPurelyPassiveSkill(IGameObject skill);
    }
}