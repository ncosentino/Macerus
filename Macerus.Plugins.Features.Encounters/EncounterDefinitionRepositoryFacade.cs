using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterDefinitionRepositoryFacade : IEncounterDefinitionRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableEncounterDefinitionRepository> _repositories;

        public EncounterDefinitionRepositoryFacade(IEnumerable<IDiscoverableEncounterDefinitionRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IEncounterDefinition GetEncounterDefinitionById(IIdentifier encounterDefinitionId)
        {
            var match = _repositories
                .Select(x => x.GetEncounterDefinitionById(encounterDefinitionId))
                .FirstOrDefault(x => x != null);
            if (match == null)
            {
                throw new KeyNotFoundException(
                    $"Could not get an instance of '{typeof(IEncounterDefinition)}' " +
                    $"with ID '{encounterDefinitionId}'.");
            }

            return match;
        }
    }
}
