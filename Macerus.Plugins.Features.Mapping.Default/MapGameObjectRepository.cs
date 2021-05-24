using System.Collections.Generic;

using Macerus.Api.Behaviors;
using Macerus.Api.GameObjects;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Mapping.Default
{
    public sealed class MapGameObjectRepository : IMapGameObjectRepository
    {
        private readonly IGameObjectRepositoryAmenity _gameObjectRepositoryAmenity;
        private readonly ILogger _logger;
        private readonly IMapResourceIdConverter _mapResourceIdConverter;
        private readonly IMapResourceLoader _resourceLoader;
        private readonly IDeserializer _deserializer;
        private readonly Dictionary<IIdentifier, List<IIdentifier>> _gameObjectIdCache;

        public MapGameObjectRepository(
            IGameObjectRepositoryAmenity gameObjectRepositoryAmenity,
            ILogger logger,
            IMapResourceIdConverter mapResourceIdConverter,
            IMapResourceLoader resourceLoader,
            IDeserializer deserializer)
        {
            _gameObjectRepositoryAmenity = gameObjectRepositoryAmenity;
            _logger = logger;
            _mapResourceIdConverter = mapResourceIdConverter;
            _resourceLoader = resourceLoader;
            _deserializer = deserializer;
            _gameObjectIdCache = new Dictionary<IIdentifier, List<IIdentifier>>();
        }

        public IEnumerable<IGameObject> LoadForMap(IIdentifier mapId)
        {
            if (_gameObjectIdCache.TryGetValue(
                mapId,
                out var cachedGameObjectIds))
            {
                foreach (var gameObjectId in cachedGameObjectIds)
                {
                    var gameObject = _gameObjectRepositoryAmenity.LoadGameObject(gameObjectId);
                    yield return gameObject;
                }

                yield break;
            }

            var mapResourcePath = _mapResourceIdConverter.ConvertToGameObjectResourcePath(mapId.ToString());

            IReadOnlyCollection<IGameObject> gameObjects;
            using (var mapResourceStream = _resourceLoader.LoadStream(mapResourcePath))
            {
                gameObjects = _deserializer.Deserialize<IReadOnlyCollection<IGameObject>>(mapResourceStream);
            }

            foreach (var gameObject in gameObjects)
            {
                if (gameObject.TryGetFirst<IReadOnlyTemplateIdentifierBehavior>(out var templateIdentifierBehavior) &&
                    !gameObject.Has<ICreatedFromTemplateBehavior>())
                {
                    var templatedGameObject = _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                        templateIdentifierBehavior.TemplateId,
                        gameObject.Behaviors);
                    yield return templatedGameObject;
                    continue;
                }

                yield return gameObject;
            }
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
    }
}