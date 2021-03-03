using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public interface ISkillAmenity
    {
        IGameObject GetSkillById(IIdentifier skillDefinitionId);
    }
}