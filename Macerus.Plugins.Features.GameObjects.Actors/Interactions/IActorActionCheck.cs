using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Interactions
{
    public interface IActorActionCheck
    {
        bool CanAct(
            IFilterContext filterContext,
            IGameObject actor);
    }
}
