using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

namespace Macerus.Plugins.Features.GameObjects.Actors.Triggers
{
    public interface ILevelUpTriggerMechanic : IDiscoverableTriggerMechanic
    {
        Task ActorLevelUpTriggeredAsync(
            IGameObject actor,
            int level);
    }
}
