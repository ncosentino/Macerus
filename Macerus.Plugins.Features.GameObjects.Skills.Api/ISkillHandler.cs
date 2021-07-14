using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ISkillHandler
    {
        Task HandleAsync(
            IGameObject user,
            IGameObject skill);
    }
}
