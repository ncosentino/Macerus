using System;
using System.Collections.Generic;
using System.Globalization;

using Macerus.Api.GameObjects;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public sealed class TiledNetGameObjectRepository : IMapGameObjectRepository
    {
        private readonly ITiledMapLoader _tiledMapLoader;
        private readonly IGameObjectRepositoryAmenity _gameObjectRepositoryAmenity;
        private readonly ILogger _logger;

        public TiledNetGameObjectRepository(
            ITiledMapLoader tiledMapLoader,
            IGameObjectRepositoryAmenity gameObjectRepositoryAmenity,
            ILogger logger)
        {
            _tiledMapLoader = tiledMapLoader;
            _gameObjectRepositoryAmenity = gameObjectRepositoryAmenity;
            _logger = logger;
        }

        public IEnumerable<IGameObject> LoadForMap(IIdentifier mapId)
        {
            var tiledMap = _tiledMapLoader.LoadMap(mapId);

            double worldScalingFactor;
            if (tiledMap.Properties.TryGetValue("WorldScalingFactor", out var rawWorldScalingFactor))
            {
                worldScalingFactor = Convert.ToDouble(
                    rawWorldScalingFactor,
                    CultureInfo.InvariantCulture);
            }
            else
            {
                worldScalingFactor = 1;
                _logger.Warn(
                    $"No world scaling factor set for map '{mapId}'. Using the " +
                    $"default of {worldScalingFactor}.");
            }

            var worldScalingFactorX = worldScalingFactor * tiledMap.TileWidth;
            var worldScalingFactorY = worldScalingFactor * tiledMap.TileHeight;
            _logger.Debug(
                $"Using world scaling factor {worldScalingFactor} " +
                $"(x={worldScalingFactorX}, y={worldScalingFactorY}) for map " +
                $"'{mapId}'.");

            foreach (var tiledMapObjectLayer in tiledMap.ObjectLayers)
            {
                foreach (var tiledMapObject in tiledMapObjectLayer.Objects)
                {
                    var properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                    {
                        ["X"] = tiledMapObject.X / worldScalingFactorX,
                        ["Y"] = tiledMapObject.Y / worldScalingFactorY,
                        ["Width"] = tiledMapObject.Width / worldScalingFactorX,
                        ["Height"] = tiledMapObject.Height / worldScalingFactorY,
                        ["$TiledId"] = tiledMapObject.Id,
                        ["$TiledType"] = tiledMapObject.Type,
                    };
                    foreach (var property in tiledMapObject.Properties)
                    {
                        properties[property.Key.TrimStart('$')] = property.Value;
                    }

                    if (!properties.TryGetValue("TypeId", out var typeId))
                    {
                        throw new InvalidOperationException(
                            $"No type ID found on tiled object '{tiledMapObject.Id}' " +
                            $"on map '{mapId}'.");
                    }

                    if (properties.TryGetValue("TemplateId", out var templateId))
                    {
                        var gameObjectFromTemplate = _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                            new StringIdentifier(typeId.ToString()), // FIXME: assuming string is a bit hacky
                            new StringIdentifier(templateId.ToString()),
                            properties); // FIXME: assuming string is a bit hacky
                        yield return gameObjectFromTemplate;
                        continue;
                    }

                    if (!properties.TryGetValue("UniqueId", out var uniqueId))
                    {
                        throw new InvalidOperationException(
                            $"No unique ID found on tiled object '{tiledMapObject.Id}' " +
                            $"on map '{mapId}'.");
                    }

                    var gameObject = _gameObjectRepositoryAmenity.LoadSingleGameObject(
                        new StringIdentifier(typeId.ToString()), // FIXME: assuming string is a bit hacky
                        new StringIdentifier(uniqueId.ToString())); // FIXME: assuming string is a bit hacky
                    yield return gameObject;
                }
            }
        }
    }
}