using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class EncounterSpawnLocationBehavior :
        BaseBehavior,
        IEncounterSpawnLocationBehavior
    {
        public EncounterSpawnLocationBehavior(IEnumerable<int> allowedTeams)
        {
            AllowedTeams = allowedTeams.ToArray();
        }

        public IReadOnlyCollection<int> AllowedTeams { get; }
    }
}
