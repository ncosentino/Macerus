using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.TurnBased;

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

        public async Task HandleAsync(
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
