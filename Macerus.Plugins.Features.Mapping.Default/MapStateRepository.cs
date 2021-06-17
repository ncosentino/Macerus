using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Mapping.Default
{
    public sealed class MapStateRepository : IMapStateRepository
    {
        private readonly Dictionary<IIdentifier, List<IIdentifier>> _gameObjectIdCache;

        public MapStateRepository()
        {
            _gameObjectIdCache = new Dictionary<IIdentifier, List<IIdentifier>>();
        }

        public void SaveState(
           IGameObject map,
           IEnumerable<IGameObject> gameObjects)
        {
            if (map.Has<IIgnoreSavingGameObjectStateBehavior>())
            {
                return;
            }

            var mapId = map.GetOnly<IIdentifierBehavior>().Id;
            if (!_gameObjectIdCache.TryGetValue(
                mapId,
                out var cachedMapGameObjectes))
            {
                cachedMapGameObjectes = new List<IIdentifier>();
                _gameObjectIdCache[mapId] = cachedMapGameObjectes;
            }

            cachedMapGameObjectes.Clear();
            foreach (var gameObject in gameObjects)
            {
                var identifierBehavior = gameObject.GetOnly<IIdentifierBehavior>();
                var id = identifierBehavior.Id;
                cachedMapGameObjectes.Add(id);
            }
        }

        public bool HasState(IIdentifier mapId) => _gameObjectIdCache.ContainsKey(mapId);

        public IEnumerable<KeyValuePair<IIdentifier, IReadOnlyCollection<IIdentifier>>> GetAllState() => _gameObjectIdCache
            .Select(x => new KeyValuePair<IIdentifier, IReadOnlyCollection<IIdentifier>>(
                x.Key,
                x.Value));

        public IEnumerable<IIdentifier> GetState(IIdentifier mapId) =>
            _gameObjectIdCache.TryGetValue(
                mapId,
                out var cachedState)
            ? cachedState
            : Enumerable.Empty<IIdentifier>();

        public void ClearState() => _gameObjectIdCache.Clear();
    }
}