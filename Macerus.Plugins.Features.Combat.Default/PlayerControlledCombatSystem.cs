using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class PlayerControlledCombatSystem : IDiscoverableSystem
    {
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly Lazy<IReadOnlyRosterManager> _lazyRosterManager;

        public int? Priority => null;

        public PlayerControlledCombatSystem(
            IObservableCombatTurnManager combatTurnManager,
            Lazy<IReadOnlyRosterManager> lazyRosterManager)
        {
            _combatTurnManager = combatTurnManager;
            _lazyRosterManager = lazyRosterManager;
            _combatTurnManager.TurnProgressed += CombatTurnManager_TurnProgressed;
            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            _combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
        }

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
        }

        private void SetSingleActivePlayerControlled(IGameObject target)
        {
            foreach (var actor in _lazyRosterManager.Value.FullRoster)
            {
                IPlayerControlledBehavior playerControlledBehavior = null;
                if (actor?.TryGetFirst(out playerControlledBehavior) != true)
                {
                    continue;
                }

                if (playerControlledBehavior == null)
                {
                    continue;
                }

                var active = actor == target;
                playerControlledBehavior.IsActive = active;
            }
        }

        private void CombatTurnManager_CombatEnded(
            object sender,
            CombatEndedEventArgs e)
        {
            SetSingleActivePlayerControlled(_lazyRosterManager
                .Value
                .ActivePartyLeader);
        }

        private void CombatTurnManager_TurnProgressed(
            object sender,
            TurnProgressedEventArgs e)
        {
            var nextActor = e.ActorWithNextTurn;
            SetSingleActivePlayerControlled(nextActor);
        }

        private void CombatTurnManager_CombatStarted(
            object sender,
            CombatStartedEventArgs e)
        {
            var nextActor = e.ActorOrder.First();
            SetSingleActivePlayerControlled(nextActor);
        }
    }
}
