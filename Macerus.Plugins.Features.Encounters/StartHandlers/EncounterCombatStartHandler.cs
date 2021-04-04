
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterCombatStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly ITurnBasedManager _turnBasedManager;
        private readonly ICombatTurnManager _combatTurnManager;

        public EncounterCombatStartHandler(
            ICombatTurnManager combatTurnManager,
            ITurnBasedManager turnBasedManager)
        {
            _combatTurnManager = combatTurnManager;
            _turnBasedManager = turnBasedManager;
        }

        public int Priority => int.MaxValue - 10000;

        public void Handle(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            _turnBasedManager.ClearApplicableOnUpdate = true;
            _turnBasedManager.GlobalSync = false;
            _turnBasedManager.SyncTurnsFromElapsedTime = false;

            _combatTurnManager.StartCombat(filterContext);
        }
    }
}
