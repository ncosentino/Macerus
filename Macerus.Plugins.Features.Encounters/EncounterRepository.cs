using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterRepository : IEncounterRepository
    {
        private readonly IGeneratorComponentToBehaviorConverterFacade _generatorComponentToBehaviorConverter;
        private readonly IEncounterDefinitionRepositoryFacade _encounterDefinitionRepositoryFacade;
        private readonly IGameObjectFactory _gameObjectFactory;

        public EncounterRepository(
            IGeneratorComponentToBehaviorConverterFacade generatorComponentToBehaviorConverter,
            IEncounterDefinitionRepositoryFacade encounterDefinitionRepositoryFacade,
            IGameObjectFactory gameObjectFactory)
        {
            _generatorComponentToBehaviorConverter = generatorComponentToBehaviorConverter;
            _encounterDefinitionRepositoryFacade = encounterDefinitionRepositoryFacade;
            _gameObjectFactory = gameObjectFactory;
        }

        public IGameObject GetEncounterById(IIdentifier encounterDefinitionId)
        {
            var encounterDefinition = _encounterDefinitionRepositoryFacade.GetEncounterDefinitionById(encounterDefinitionId);
            var behaviors = _generatorComponentToBehaviorConverter.Convert(
                Enumerable.Empty<IBehavior>(),
                encounterDefinition.GeneratorComponents);
            var encounter = _gameObjectFactory.Create(behaviors);
            return encounter;
        }
    }
}
