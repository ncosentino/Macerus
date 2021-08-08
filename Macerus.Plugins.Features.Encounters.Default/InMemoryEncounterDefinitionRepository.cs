using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class InMemoryEncounterDefinitionRepository : IDiscoverableEncounterDefinitionRepository
    {
        private Dictionary<IIdentifier, IEncounterDefinition> _encounterDefinitions;

        public InMemoryEncounterDefinitionRepository(IEnumerable<IEncounterDefinition> encounterDefinitions)
        {
            _encounterDefinitions = encounterDefinitions.ToDictionary(
                x => x.Id,
                x => x);
        }

        public IEncounterDefinition GetEncounterDefinitionById(IIdentifier encounterDefinitionId)
        {
            if (_encounterDefinitions.TryGetValue(
                encounterDefinitionId,
                out var match))
            {
                return match;
            }

            return null;
        }
    }
}
