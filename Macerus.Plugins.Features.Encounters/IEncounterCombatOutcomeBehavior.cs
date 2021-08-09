using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterCombatOutcomeBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> WinningTeam { get; }

        IReadOnlyDictionary<int, IReadOnlyCollection<IGameObject>> LosingTeams { get; }

        bool? PlayerWon { get; }
    }
}
