using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public interface ISkillUsage
    {
        Task<bool> CanUseSkillAsync(
            IGameObject actor,
            IGameObject skill);

        Task UseRequiredResourcesAsync(IGameObject actor, IGameObject skill);
    }
}