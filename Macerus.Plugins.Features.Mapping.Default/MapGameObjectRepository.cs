using System.Collections.Generic;
using System.Threading.Tasks;

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
        private readonly IMapStateRepository _mapStateRepository;
        private readonly ILogger _logger;
        private readonly IMapResourceIdConverter _mapResourceIdConverter;
        private readonly IMapResourceLoader _resourceLoader;
        private readonly IDeserializer _deserializer;

        public MapGameObjectRepository(
            IGameObjectRepositoryAmenity gameObjectRepositoryAmenity,
            IMapStateRepository mapStateRepository,
            ILogger logger,
            IMapResourceIdConverter mapResourceIdConverter,
            IMapResourceLoader resourceLoader,
            IDeserializer deserializer)
        {
            _gameObjectRepositoryAmenity = gameObjectRepositoryAmenity;
            _mapStateRepository = mapStateRepository;
            _logger = logger;
            _mapResourceIdConverter = mapResourceIdConverter;
            _resourceLoader = resourceLoader;
            _deserializer = deserializer;
        }

        public async Task<IReadOnlyCollection<IGameObject>> LoadForMapAsync(IIdentifier mapId)
        {
            if (_mapStateRepository.HasState(mapId))
            {
                var accumulateFromState = new List<IGameObject>();
                foreach (var gameObjectId in _mapStateRepository.GetState(mapId))
                {
                    var gameObject = _gameObjectRepositoryAmenity.LoadGameObject(gameObjectId);
                    accumulateFromState.Add(gameObject);
                }

                return accumulateFromState;
            }

            var mapResourcePath = _mapResourceIdConverter.ConvertToGameObjectResourcePath(mapId.ToString());

            IReadOnlyCollection<IGameObject> gameObjects;
            using (var mapResourceStream = await _resourceLoader
                .LoadStreamAsync(mapResourcePath)
                .ConfigureAwait(false))
            {
                gameObjects = _deserializer.Deserialize<IReadOnlyCollection<IGameObject>>(mapResourceStream);
            }

            var accumulateFromLoad = new List<IGameObject>();
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.TryGetFirst<IReadOnlyTemplateIdentifierBehavior>(out var templateIdentifierBehavior) &&
                    !gameObject.Has<ICreatedFromTemplateBehavior>())
                {
                    var templatedGameObject = _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                        templateIdentifierBehavior.TemplateId,
                        gameObject.Behaviors);
                    accumulateFromLoad.Add(templatedGameObject);
                    continue;
                }

                accumulateFromLoad.Add(gameObject);
            }

            return accumulateFromLoad;
        }       
    }
}