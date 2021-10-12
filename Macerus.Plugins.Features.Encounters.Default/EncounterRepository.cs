using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors;

namespace Macerus.Plugins.Features.Encounters.Default
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

        public IGameObject GetEncounterById(
            IFilterContext filterContext,
            IIdentifier encounterDefinitionId)
        {
            var encounterDefinition = _encounterDefinitionRepositoryFacade.GetEncounterDefinitionById(encounterDefinitionId);
            var behaviors = _generatorComponentToBehaviorConverter.Convert(
                filterContext,
                new IBehavior[]
                {
                    new IdentifierBehavior(encounterDefinitionId),
                },
                encounterDefinition.GeneratorComponents);
            var encounter = _gameObjectFactory.Create(behaviors);
            return encounter;
        }
    }
}
