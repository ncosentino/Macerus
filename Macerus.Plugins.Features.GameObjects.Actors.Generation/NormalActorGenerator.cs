using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class NormalActorGenerator : IDiscoverableActorGenerator
    {
        private readonly IActorDefinitionRepositoryFacade _actorDefinitionRepositoryFacade;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IGeneratorComponentToBehaviorConverterFacade _generatorComponentToBehaviorConverter;
        private readonly IActorFactory _actorFactory;

        public NormalActorGenerator(
            IActorDefinitionRepositoryFacade actorDefinitionRepositoryFacade,
            IFilterContextAmenity filterContextAmenity,
            IGeneratorComponentToBehaviorConverterFacade generatorComponentToBehaviorConverter,
            IActorFactory actorFactory)
        {
            _actorDefinitionRepositoryFacade = actorDefinitionRepositoryFacade;
            _filterContextAmenity = filterContextAmenity;
            _generatorComponentToBehaviorConverter = generatorComponentToBehaviorConverter;
            _actorFactory = actorFactory;

            SupportedAttributes = new IFilterAttribute[]
            {
                filterContextAmenity.CreateSupportedAttribute(
                    new StringIdentifier("affix-type"),
                    "normal"),
            };
        }

        public IEnumerable<IGameObject> GenerateActors(IFilterContext filterContext)
        {
            var actorDefinitions = _actorDefinitionRepositoryFacade.GetActorDefinitions(filterContext);

            foreach (var actorDefinition in actorDefinitions)
            {
                var definitionBehaviors = _generatorComponentToBehaviorConverter.Convert(
                    Enumerable.Empty<IBehavior>(),
                    actorDefinition.GeneratorComponents);
                var actor = _actorFactory.Create(
                    new IdentifierBehavior(new StringIdentifier(Guid.NewGuid().ToString())), // FIXME: do we really need a guid here...
                    definitionBehaviors);
                yield return actor;
            }
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}
