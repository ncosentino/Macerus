using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterSpawnLocationBehavior : IBehavior
    {
        IReadOnlyCollection<int> AllowedTeams { get; }
    }
}
