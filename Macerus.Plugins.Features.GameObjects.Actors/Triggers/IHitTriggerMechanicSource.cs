using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

namespace Macerus.Plugins.Features.GameObjects.Actors.Triggers
{
    public interface IHitTriggerMechanicSource :
        ITriggerSourceMechanic,
        IDiscoverableTriggerMechanicRegistrar
    {
        Task ActorHitTriggeredAsync(
            IGameObject attacker,
            IGameObject defender,
            IGameObject skill);
    }
}
