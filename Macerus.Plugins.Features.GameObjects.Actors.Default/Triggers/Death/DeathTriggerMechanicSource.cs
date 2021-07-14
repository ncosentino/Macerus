using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors.Triggers;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Death
{
    public sealed class DeathTriggerMechanicSource : IDeathTriggerMechanicSource
    {
        private readonly ConcurrentDictionary<IDeathTriggerMechanic, bool> _deathTriggerMechanics;

        public DeathTriggerMechanicSource()
        {
            _deathTriggerMechanics = new ConcurrentDictionary<IDeathTriggerMechanic, bool>();
        }

        public bool CanRegister(ITriggerMechanic triggerMechanic) => triggerMechanic is IDeathTriggerMechanic;

        public void RegisterTrigger(ITriggerMechanic triggerMechanic) => _deathTriggerMechanics.TryAdd(
            (IDeathTriggerMechanic)triggerMechanic,
            true);

        public void UnregisterTrigger(ITriggerMechanic triggerMechanic) => _deathTriggerMechanics.TryRemove(
            (IDeathTriggerMechanic)triggerMechanic,
            out _);

        public async Task ActorDeathTriggeredAsync(IGameObject actor)
        {
            var tasks = _deathTriggerMechanics
                .Keys
                .Select(x => x.ActorDeathTriggeredAsync(actor));
            await Task
                .WhenAll(tasks)
                .ConfigureAwait(false);
        }
    }
}
