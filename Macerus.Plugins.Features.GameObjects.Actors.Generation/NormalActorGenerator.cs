using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Actors.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

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
                
                // FIXME: this whole part around the ID feels like complete
                // trash. what's the right balance between allowing definitions
                // to specify the ID but also being safe to ensure there is one?
                var definitionBehaviors = _generatorComponentToBehaviorConverter
                    .Convert(
                        filterContext,
                        Enumerable.Empty<IBehavior>(),
                        generatorComponents)
                    .ToArray();
                var identifierBehavior = definitionBehaviors
                    .TakeTypes<IReadOnlyIdentifierBehavior>()
                    .SingleOrDefault()
                    ?? new IdentifierBehavior(new StringIdentifier(Guid.NewGuid().ToString())); // FIXME: do we really need a guid here...
                var actor = _actorFactory.Create(
                    identifierBehavior,
                    definitionBehaviors.Except(new[] { identifierBehavior }));
                yield return actor;
            }
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}
