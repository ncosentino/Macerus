using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors.Triggers;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Triggers
{
    public sealed class LevelUpMechanicSource : ILevelUpTriggerMechanicSource
    {
        private readonly ConcurrentDictionary<ILevelUpTriggerMechanic, bool> _levelUpTriggerMechanics;

        public LevelUpMechanicSource()
        {
            _levelUpTriggerMechanics = new ConcurrentDictionary<ILevelUpTriggerMechanic, bool>();
        }

        public bool CanRegister(ITriggerMechanic triggerMechanic) => triggerMechanic is ILevelUpTriggerMechanic;

        public void RegisterTrigger(ITriggerMechanic triggerMechanic) => _levelUpTriggerMechanics.TryAdd(
            (ILevelUpTriggerMechanic)triggerMechanic,
            true);

        public void UnregisterTrigger(ITriggerMechanic triggerMechanic) => _levelUpTriggerMechanics.TryRemove(
            (ILevelUpTriggerMechanic)triggerMechanic,
            out _);

        public async Task ActorLevelUpTriggeredAsync(IGameObject actor, int level)
        {
            var tasks = _levelUpTriggerMechanics
                .Keys
                .Select(x => x.ActorLevelUpTriggeredAsync(actor, level));
            await Task
                .WhenAll(tasks)
                .ConfigureAwait(false);
        }
    }
}
