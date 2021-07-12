using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

namespace Macerus.Plugins.Features.GameObjects.Actors.Death
{
    public interface IDeathTriggerMechanicSource :
        ITriggerSourceMechanic,
        IDiscoverableTriggerMechanicRegistrar
    {
        Task ActorDeathTriggeredAsync(IGameObject actor);
    }
}
