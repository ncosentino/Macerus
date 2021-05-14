using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.GameObjects;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;

namespace Macerus.Game
{
    public sealed class InMemoryGameObjectTemplateRepository : IGameObjectTemplateRepository
    {
        private readonly IGameObjectIdentifiers _gameObjectIdentifiers;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IReadOnlyDictionary<IIdentifier, Func<IGameObject>> _templateObjectFactoriesById;

        public InMemoryGameObjectTemplateRepository(
            IGameObjectIdentifiers gameObjectIdentifiers,
            IAttributeFilterer attributeFilterer,
            IEnumerable<KeyValuePair<IIdentifier, Func<IGameObject>>> templateObjectFactoriesById)
        {
            _gameObjectIdentifiers = gameObjectIdentifiers;
            _attributeFilterer = attributeFilterer;
            _templateObjectFactoriesById = templateObjectFactoriesById.ToDictionary(x => x.Key, x => x.Value);
        }

        public IEnumerable<IGameObject> GetTemplates(IFilterContext filterContext)
        {
            var requiredAttributes = filterContext
                .Attributes
                .Where(x => x.Required)
                .ToArray();
            if (requiredAttributes.Length != 1 ||
                !requiredAttributes.Single().Id.Equals(_gameObjectIdentifiers.FilterContextTemplateId) ||
                !(requiredAttributes.Single().Value is IdentifierFilterAttributeValue))
            {
                throw new NotSupportedException(
                    "// FIXME: This is the API we want to encourage, but currently there " +
                    "is only support for loading templates by ID. You could be the " +
                    "hero we need. Implement filtering!");
            }

            var templateId = ((IdentifierFilterAttributeValue)requiredAttributes.Single().Value).Value;
            if (!_templateObjectFactoriesById.TryGetValue(
                templateId,
                out var templateObjectFactory))
            {
                yield break;
            }

            var templateObject = templateObjectFactory.Invoke();
            yield return templateObject;
        }
    }
}
