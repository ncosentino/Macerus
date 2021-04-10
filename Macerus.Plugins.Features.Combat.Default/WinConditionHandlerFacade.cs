using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class WinConditionHandlerFacade : IWinConditionHandlerFacade
    {
        private readonly IReadOnlyCollection<IWinConditionHandler> _winConditionHandlers;

        public WinConditionHandlerFacade(IEnumerable<IDiscoverableWinConditionHandler> winConditionHandlers)
        {
            _winConditionHandlers = winConditionHandlers.ToArray();
        }

        public bool CheckWinConditions(
            out IReadOnlyCollection<IGameObject> winningTeam,
            out IReadOnlyDictionary<int, IReadOnlyCollection<IGameObject>> losingTeams)
        {
            foreach (var handler in _winConditionHandlers)
            {
                if (handler.CheckWinConditions(
                    out winningTeam,
                    out losingTeams))
                {
                    return true;
                }
            }

            winningTeam = new IGameObject[0];
            losingTeams = new Dictionary<int, IReadOnlyCollection<IGameObject>>();
            return false;
        }
    }
}
