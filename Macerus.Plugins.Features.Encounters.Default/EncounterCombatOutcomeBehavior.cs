using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class EncounterCombatOutcomeBehavior :
        BaseBehavior,
        IEncounterCombatOutcomeBehavior
    {
        public EncounterCombatOutcomeBehavior(
            IReadOnlyCollection<IGameObject> winningTeam,
            IReadOnlyDictionary<int, IReadOnlyCollection<IGameObject>> losingTeams,
            bool? playerWon)
        {
            WinningTeam = winningTeam;
            LosingTeams = losingTeams;
            PlayerWon = playerWon;
        }

        public IReadOnlyCollection<IGameObject> WinningTeam { get; }
        
        public IReadOnlyDictionary<int, IReadOnlyCollection<IGameObject>> LosingTeams { get; }

        public bool? PlayerWon { get; }
    }
}
