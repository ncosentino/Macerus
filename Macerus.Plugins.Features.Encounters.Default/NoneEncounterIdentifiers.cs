using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class NoneEncounterIdentifiers : IEncounterIdentifiers
    {
        public IIdentifier FilterEncounterDefinitionId => new StringIdentifier(nameof(FilterEncounterDefinitionId));

        public IIdentifier FilterEncounterCombatPlayerWonId => new StringIdentifier(nameof(FilterEncounterCombatPlayerWonId));
    }
}
