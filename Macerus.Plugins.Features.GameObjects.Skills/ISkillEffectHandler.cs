using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public interface ISkillEffectHandler
    {
        Task HandleAsync(
            IGameObject user,
            IGameObject skillEffect);
    }
}
