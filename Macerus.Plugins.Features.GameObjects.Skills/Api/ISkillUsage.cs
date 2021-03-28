using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ISkillUsage
    {
        bool CanUseSkill(IGameObject actor, IGameObject skill);
        
        void UseRequiredResources(IGameObject actor, IGameObject skill);
    }
}