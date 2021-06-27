using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public interface ISkillHandler
    {
        Task HandleAsync(
            IGameObject user,
            IGameObject skill);
    }
}
