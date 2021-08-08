using System.Threading.Tasks;

using Macerus.Plugins.Features.Encounters.EndHandlers;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Plugins.Features.Encounters.Default.EndHandlers
{
    public sealed class EncounterTurnBasedEndHandler : IDiscoverableEndEncounterHandler
    {
        private readonly ITurnBasedManager _turnBasedManager;

        public EncounterTurnBasedEndHandler(ITurnBasedManager turnBasedManager)
        {
            _turnBasedManager = turnBasedManager;
        }

        public async Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            _turnBasedManager.SyncTurnsFromElapsedTime = true;
        }
    }
}
