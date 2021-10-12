using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Interactions
{
    public interface IActorInteractionManager
    {
        Task<bool> TryInteractAsync(
            IFilterContext filterContext,
            IGameObject actor);

        Task ObjectEnterInteractionRadiusAsync(
            IFilterContext filterContext,
            IGameObject actor,
            IGameObject gameObject);

        Task ObjectExitInteractionRadiusAsync(
            IFilterContext filterContext,
            IGameObject actor,
            IGameObject gameObject);
    }
}
