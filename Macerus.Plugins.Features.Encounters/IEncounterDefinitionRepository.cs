using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterDefinitionRepository
    {
        IEncounterDefinition GetEncounterDefinitionById(IIdentifier encounterDefinitionId);
    }
}
