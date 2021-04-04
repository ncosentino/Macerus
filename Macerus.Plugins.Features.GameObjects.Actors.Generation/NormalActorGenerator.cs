using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class NormalActorGenerator : IDiscoverableActorGenerator
    {
        private readonly IActorDefinitionRepositoryFacade _actorDefinitionRepositoryFacade;
        private readonly IGeneratorComponentToBehaviorConverterFacade _generatorComponentToBehaviorConverter;
        private readonly IActorFactory _actorFactory;

        public NormalActorGenerator(
            IActorDefinitionRepositoryFacade actorDefinitionRepositoryFacade,
            IFilterContextAmenity filterContextAmenity,
            IGeneratorComponentToBehaviorConverterFacade generatorComponentToBehaviorConverter,
            IActorFactory actorFactory)
        {
            _actorDefinitionRepositoryFacade = actorDefinitionRepositoryFacade;
            _generatorComponentToBehaviorConverter = generatorComponentToBehaviorConverter;
            _actorFactory = actorFactory;

            SupportedAttributes = new IFilterAttribute[]
            {
                filterContextAmenity.CreateSupportedAttribute(
                    new StringIdentifier("affix-type"),
                    "normal"),
            };
        }

        public IEnumerable<IGameObject> GenerateActors(
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents)
        {
            var actorDefinitions = _actorDefinitionRepositoryFacade.GetActorDefinitions(filterContext);

            foreach (var actorDefinition in actorDefinitions)
            {
                var generatorComponents = actorDefinition
                    .GeneratorComponents
                    .Concat(additionalActorGeneratorComponents);
                var definitionBehaviors = _generatorComponentToBehaviorConverter.Convert(
                    Enumerable.Empty<IBehavior>(),
                    generatorComponents);
                var actor = _actorFactory.Create(
                    new IdentifierBehavior(new StringIdentifier(Guid.NewGuid().ToString())), // FIXME: do we really need a guid here...
                    definitionBehaviors);
                yield return actor;
            }
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}
