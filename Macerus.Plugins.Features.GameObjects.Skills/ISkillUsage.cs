using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public interface ISkillUsage
    {
        Task<bool> CanUseSkillAsync(
            IFilterContext filterContext,
            IGameObject actor,
            IGameObject skill);

        Task UseRequiredResourcesAsync(
            IGameObject actor,
            IGameObject skill);
    }
}