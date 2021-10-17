using Macerus.Plugins.Features.Encounters;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Encounters
{
    public sealed class EncounterIdentifiers : IEncounterIdentifiers
    {
        public IIdentifier FilterEncounterDefinitionId => new StringIdentifier("encounter-id");

        public IIdentifier FilterEncounterCombatPlayerWonId => new StringIdentifier("encounter-player-won");
    }
}
