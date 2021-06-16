using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Interactions.Api
{
    public interface IInteractionHandler
    {
        Task InteractAsync(
            IGameObject actor,
            IInteractableBehavior behavior);
    }
}