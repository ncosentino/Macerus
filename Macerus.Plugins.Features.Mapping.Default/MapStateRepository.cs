using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping;

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
           IIdentifier mapId,
           IEnumerable<IIdentifier> gameObjectIds)
        {
            if (!_gameObjectIdCache.TryGetValue(
                mapId,
                out var cachedMapGameObjectes))
            {
                cachedMapGameObjectes = new List<IIdentifier>();
                _gameObjectIdCache[mapId] = cachedMapGameObjectes;
            }

            cachedMapGameObjectes.Clear();
            foreach (var id in gameObjectIds)
            {
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