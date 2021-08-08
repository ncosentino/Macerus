using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Camera;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class PlayerControlledCombatSystem : IDiscoverableSystem
    {
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly Lazy<IRosterManager> _lazyRosterManager;
        private readonly Lazy<ICameraManager> _lazyCameraManager;

        public int? Priority => null;

        public PlayerControlledCombatSystem(
            IObservableCombatTurnManager combatTurnManager,
            Lazy<IRosterManager> lazyRosterManager,
            Lazy<ICameraManager> lazyCameraManager)
        {
            _combatTurnManager = combatTurnManager;
            _lazyRosterManager = lazyRosterManager;
            _lazyCameraManager = lazyCameraManager;
            _combatTurnManager.TurnProgressed += CombatTurnManager_TurnProgressed;
            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            _combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
        }

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
        }

        private void ActivatePlayerOrFollowNpc(IGameObject actor)
        {
            if (actor.TryGetFirst<IPlayerControlledBehavior>(out var playerControlledBehavior))
            {
                playerControlledBehavior.IsActive = true;
            }
            else
            {
                _lazyCameraManager.Value.SetFollowTarget(actor);
            }
        }
      
        private void CombatTurnManager_CombatEnded(
            object sender,
            CombatEndedEventArgs e)
        {
            var leader = _lazyRosterManager
                .Value
                .ActivePartyLeader;
            if (leader != null)
            {
                leader.GetOnly<IPlayerControlledBehavior>()
                .IsActive = true;
            }
        }

        private void CombatTurnManager_TurnProgressed(
            object sender,
            TurnProgressedEventArgs e)
        {
            var nextActor = e.ActorWithNextTurn;
            ActivatePlayerOrFollowNpc(nextActor);
        }

        private void CombatTurnManager_CombatStarted(
            object sender,
            CombatStartedEventArgs e)
        {
            var nextActor = e.ActorOrder.First();
            ActivatePlayerOrFollowNpc(nextActor);
        }
    }
}
