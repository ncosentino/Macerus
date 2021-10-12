using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Interactions.Api
{
    public interface IInteractionHandler
    {
        Task InteractAsync(
            IFilterContext filterContext,
            IGameObject actor,
            IInteractableBehavior behavior);
    }
}