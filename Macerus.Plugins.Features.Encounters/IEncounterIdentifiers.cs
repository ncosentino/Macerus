using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterIdentifiers
    {
        IIdentifier FilterEncounterDefinitionId { get; }

        IIdentifier FilterEncounterCombatPlayerWonId { get; }
    }
}
