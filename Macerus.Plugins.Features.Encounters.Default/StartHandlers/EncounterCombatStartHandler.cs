using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters.Default.StartHandlers
{
    public sealed class EncounterCombatStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly ICombatTurnManager _combatTurnManager;

        public EncounterCombatStartHandler(ICombatTurnManager combatTurnManager)
        {
            _combatTurnManager = combatTurnManager;
        }

        public async Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            if (!encounter.TryGetFirst<IEncounterCombatBehavior>(out var combatBehavior))
            {
                return;
            }

            _combatTurnManager.StartCombat(filterContext);
        }
    }
}
