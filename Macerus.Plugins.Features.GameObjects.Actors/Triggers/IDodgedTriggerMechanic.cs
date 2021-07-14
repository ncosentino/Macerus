using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

namespace Macerus.Plugins.Features.GameObjects.Actors.Triggers
{
    public interface IDodgedTriggerMechanic : IDiscoverableTriggerMechanic
    {
        Task ActorMissedTriggeredAsync(
            IGameObject attacker,
            IGameObject defender);
    }
}
