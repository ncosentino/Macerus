using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public interface ISkillHandlerFacade
    {
        Task HandleSkillAsync(
            IGameObject user,
            IGameObject skill);
    }
}
