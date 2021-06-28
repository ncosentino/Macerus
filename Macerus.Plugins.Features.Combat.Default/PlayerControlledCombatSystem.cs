using System;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class PlayerControlledCombatSystem : IDiscoverableSystem
    {
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly Lazy<IRosterManager> _lazyRosterManager;

        public int? Priority => null;

        public PlayerControlledCombatSystem(
            IObservableCombatTurnManager combatTurnManager,
            Lazy<IRosterManager> lazyRosterManager)
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
      
        private void CombatTurnManager_CombatEnded(
            object sender,
            CombatEndedEventArgs e)
        {
            _lazyRosterManager.Value.SetActorToControl(_lazyRosterManager
                .Value
                .ActivePartyLeader);
        }

        private void CombatTurnManager_TurnProgressed(
            object sender,
            TurnProgressedEventArgs e)
        {
            var nextActor = e.ActorWithNextTurn;
            _lazyRosterManager.Value.SetActorToControl(nextActor);
        }

        private void CombatTurnManager_CombatStarted(
            object sender,
            CombatStartedEventArgs e)
        {
            var nextActor = e.ActorOrder.First();
            _lazyRosterManager.Value.SetActorToControl(nextActor);
        }
    }
}
