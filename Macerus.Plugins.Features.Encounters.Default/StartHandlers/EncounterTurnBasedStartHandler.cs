using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Plugins.Features.Encounters.Default.StartHandlers
{
    public sealed class EncounterTurnBasedStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly ITurnBasedManager _turnBasedManager;

        public EncounterTurnBasedStartHandler(ITurnBasedManager turnBasedManager)
        {
            _turnBasedManager = turnBasedManager;
        }

        public async Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            _turnBasedManager.SyncTurnsFromElapsedTime = false;
        }
    }
}
