using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Combat.Api
{
    public interface IWinConditionHandler
    {
        bool CheckWinConditions(
            out IReadOnlyCollection<IGameObject> winningTeam,
            out IReadOnlyDictionary<int, IReadOnlyCollection<IGameObject>> losingTeams);
    }
}
