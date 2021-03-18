using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterRepository : IEncounterRepository
    {
        private readonly IGeneratorComponentToBehaviorConverterFacade _generatorComponentToBehaviorConverter;
        private readonly IEncounterDefinitionRepositoryFacade _encounterDefinitionRepositoryFacade;
        private readonly IBehaviorManager _behaviorManager;

        public EncounterRepository(
            IGeneratorComponentToBehaviorConverterFacade generatorComponentToBehaviorConverter,
            IEncounterDefinitionRepositoryFacade encounterDefinitionRepositoryFacade,
            IBehaviorManager behaviorManager)
        {
            _generatorComponentToBehaviorConverter = generatorComponentToBehaviorConverter;
            _encounterDefinitionRepositoryFacade = encounterDefinitionRepositoryFacade;
            _behaviorManager = behaviorManager;
        }

        public IGameObject GetEncounterById(IIdentifier encounterDefinitionId)
        {
            var encounterDefinition = _encounterDefinitionRepositoryFacade.GetEncounterDefinitionById(encounterDefinitionId);
            var behaviors = _generatorComponentToBehaviorConverter.Convert(
                Enumerable.Empty<IBehavior>(),
                encounterDefinition.GeneratorComponents);
            var encounter = new Encounter(behaviors);
            _behaviorManager.Register(
                encounter,
                encounter.Behaviors);
            return encounter;
        }
    }
}
