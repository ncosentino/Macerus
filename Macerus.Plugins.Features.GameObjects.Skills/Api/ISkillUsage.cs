using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ISkillUsage
    {
        void ApplySkillEffectsToTarget(IGameObject skill, IGameObject target);
        bool CanUseSkill(IGameObject actor, IGameObject skill);
        void UseRequiredResources(IGameObject actor, IGameObject skill);
    }
}