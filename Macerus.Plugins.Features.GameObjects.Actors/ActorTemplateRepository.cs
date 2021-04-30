﻿using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorTemplateRepository : IDiscoverableGameObjectTemplateRepository
    {
        private readonly IGameObjectIdentifiers _gameObjectIdentifiers;
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IReadOnlyCollection<IDiscoverableActorTemplateRepository> _discoverableActorTemplateRepositories;

        public ActorTemplateRepository(
            IGameObjectIdentifiers gameObjectIdentifiers,
            IActorIdentifiers actorIdentifiers,
            IFilterContextAmenity filterContextAmenity,
            IAttributeFilterer attributeFilterer,
            IEnumerable<IDiscoverableActorTemplateRepository> discoverableActorTemplateRepositories)
        {
            _gameObjectIdentifiers = gameObjectIdentifiers;
            _actorIdentifiers = actorIdentifiers;
            _filterContextAmenity = filterContextAmenity;
            _attributeFilterer = attributeFilterer;
            _discoverableActorTemplateRepositories = discoverableActorTemplateRepositories.ToArray();
            
            SupportedAttributes = new[]
            {
                _filterContextAmenity.CreateSupportedAttribute(
                    _gameObjectIdentifiers.FilterContextTypeId,
                    _actorIdentifiers.ActorTypeIdentifier),
                _filterContextAmenity.CreateSupportedAlwaysMatchingAttribute(
                    _gameObjectIdentifiers.FilterContextTemplateId),
            };
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IGameObject CreateFromTemplate(
            IFilterContext filterContext,
            IReadOnlyDictionary<string, object> properties)
        {
            var subFilterContext = _filterContextAmenity.CreateSubContext(
                filterContext,
                SupportedAttributes);

            var filteredRepositories = _attributeFilterer.Filter(
                _discoverableActorTemplateRepositories,
                subFilterContext);
            var results = filteredRepositories
                .Select(x => x.CreateFromTemplate(
                    subFilterContext,
                    properties))
                .ToArray();

            if (results.Length != 1)
            {
                throw new InvalidOperationException(
                    $"Expecting only one actor to be created from template but " +
                    $"{results.Length} were created.");
            }

            return results.Single();
        }
    }
}