using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class EncounterRepository : IEncounterRepository
    {
        private readonly IGeneratorComponentToBehaviorConverterFacade _generatorComponentToBehaviorConverter;
        private readonly IEncounterDefinitionRepositoryFacade _encounterDefinitionRepositoryFacade;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IFilterContextProvider _filterContextProvider;

        public EncounterRepository(
            IGeneratorComponentToBehaviorConverterFacade generatorComponentToBehaviorConverter,
            IEncounterDefinitionRepositoryFacade encounterDefinitionRepositoryFacade,
            IGameObjectFactory gameObjectFactory,
            IFilterContextProvider filterContextProvider)
        {
            _generatorComponentToBehaviorConverter = generatorComponentToBehaviorConverter;
            _encounterDefinitionRepositoryFacade = encounterDefinitionRepositoryFacade;
            _gameObjectFactory = gameObjectFactory;
            _filterContextProvider = filterContextProvider;
        }

        public IGameObject GetEncounterById(IIdentifier encounterDefinitionId)
        {
            var encounterDefinition = _encounterDefinitionRepositoryFacade.GetEncounterDefinitionById(encounterDefinitionId);
            // FIXME: this should be passed in on the API not from a provider
            var filterContext = _filterContextProvider.GetContext();
            var behaviors = _generatorComponentToBehaviorConverter.Convert(
                filterContext,
                Enumerable.Empty<IBehavior>(),
                encounterDefinition.GeneratorComponents);
            var encounter = _gameObjectFactory.Create(behaviors);
            return encounter;
        }
    }
}
