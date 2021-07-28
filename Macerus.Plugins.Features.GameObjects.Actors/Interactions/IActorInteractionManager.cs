using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Actors.Interactions
{
    public interface IActorInteractionManager
    {
        Task<bool> TryInteractAsync(IGameObject actor);

        Task ObjectEnterInteractionRadiusAsync(
            IGameObject actor,
            IGameObject gameObject);

        Task ObjectExitInteractionRadiusAsync(
            IGameObject actor,
            IGameObject gameObject);
    }
}
