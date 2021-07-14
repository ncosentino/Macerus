using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors.Triggers;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Triggers
{
    public sealed class HitTriggerMechanicSource : IHitTriggerMechanicSource
    {
        private readonly ConcurrentDictionary<IHitTriggerMechanic, bool> _triggerMechanics;

        public HitTriggerMechanicSource()
        {
            _triggerMechanics = new ConcurrentDictionary<IHitTriggerMechanic, bool>();
        }

        public bool CanRegister(ITriggerMechanic triggerMechanic) => triggerMechanic is IHitTriggerMechanic;

        public void RegisterTrigger(ITriggerMechanic triggerMechanic) => _triggerMechanics.TryAdd(
            (IHitTriggerMechanic)triggerMechanic,
            true);

        public void UnregisterTrigger(ITriggerMechanic triggerMechanic) => _triggerMechanics.TryRemove(
            (IHitTriggerMechanic)triggerMechanic,
            out _);

        public async Task ActorHitTriggeredAsync(
            IGameObject attacker,
            IGameObject defender,
            IGameObject skill)
        {
            var tasks = _triggerMechanics
                .Keys
                .Select(x => x.ActorHitTriggeredAsync(
                    attacker,
                    defender,
                    skill));
            await Task
                .WhenAll(tasks)
                .ConfigureAwait(false);
        }
    }
}
