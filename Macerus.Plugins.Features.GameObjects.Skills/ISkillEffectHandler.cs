using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public interface ISkillEffectHandler
    {
        Task<IGameObject> HandleAsync(
            IGameObject user,
            IGameObject skill,
            IGameObject skillEffect);
    }
}
