using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Combat.Api;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class WinConditionHandlerFacade : IWinConditionHandlerFacade
    {
        private readonly IReadOnlyCollection<IWinConditionHandler> _winConditionHandlers;

        public WinConditionHandlerFacade(IEnumerable<IDiscoverableWinConditionHandler> winConditionHandlers)
        {
            _winConditionHandlers = winConditionHandlers.ToArray();
        }

        public bool TryGetWinningTeam(out double winningTeamId)
        {
            winningTeamId = -1;
            foreach (var handler in _winConditionHandlers)
            {
                if (handler.TryGetWinningTeam(out winningTeamId))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
