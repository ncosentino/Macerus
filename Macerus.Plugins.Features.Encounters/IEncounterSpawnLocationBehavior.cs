using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterSpawnLocationBehavior : IBehavior
    {
        IReadOnlyCollection<int> AllowedTeams { get; }
    }
}
