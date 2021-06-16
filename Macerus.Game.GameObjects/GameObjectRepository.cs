using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Game
{
    public sealed class GameObjectRepository : IGameObjectRepository
    {
        private readonly Dictionary<IIdentifier, IGameObject> _cache;
        private readonly IGameObjectIdentifiers _gameObjectIdentifiers;

        public GameObjectRepository(IGameObjectIdentifiers gameObjectIdentifiers)
        {
            _cache = new Dictionary<IIdentifier, IGameObject>();
            _gameObjectIdentifiers = gameObjectIdentifiers;
        }

        public void Save(IGameObject gameObject)
        {
            var identifierBehavior = gameObject.GetOnly<IIdentifierBehavior>();
            var id = identifierBehavior.Id;
            _cache[id] = gameObject;
        }

        public void Clear()
        {
            _cache.Clear();
        }

        public IEnumerable<IGameObject> LoadAll() => _cache.Values;

        public IEnumerable<IGameObject> Load(IFilterContext filterContext)
        {
            var requiredAttributes = filterContext
                .Attributes
                .Where(x => x.Required)
                .ToArray();
            if (requiredAttributes.Length != 1 ||
                !requiredAttributes.Single().Id.Equals(_gameObjectIdentifiers.FilterContextId) ||
                !(requiredAttributes.Single().Value is IdentifierFilterAttributeValue))
            {
                throw new NotSupportedException(
                    "// FIXME: This is the API we want to encourage, but currently there " +
                    "is only support for loading objects by ID. You could be the " +
                    "hero we need. Implement filtering!");
            }

            var gameObjectId = ((IdentifierFilterAttributeValue)requiredAttributes.Single().Value).Value;
            if (_cache.TryGetValue(gameObjectId, out var gameObject))
            {
                yield return gameObject;
            }
        }
    }
}
