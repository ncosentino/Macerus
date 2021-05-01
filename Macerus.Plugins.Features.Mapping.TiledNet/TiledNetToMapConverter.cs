using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Mapping.Default;

using Tiled.Net.Dto.Tilesets;
using Tiled.Net.Maps;
using Tiled.Net.Tilesets;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public sealed class TiledNetToMapConverter : ITiledNetToMapConverter
    {
        private readonly ITilesetSpriteResourceResolver _tilesetSpriteResourceResolver;
        private readonly IMapFactory _mapFactory;

        public TiledNetToMapConverter(
            ITilesetSpriteResourceResolver tilesetSpriteResourceResolver,
            IMapFactory mapFactory)
        {
            _tilesetSpriteResourceResolver = tilesetSpriteResourceResolver;
            _mapFactory = mapFactory;
        }

        public IMap Convert(
            IIdentifier mapId,
            ITiledMap tiledMap)
        {
            var flipY = tiledMap.RenderOrder.IndexOf("-down", StringComparison.OrdinalIgnoreCase) != -1
                ? -1
                : 1;
            var flipX = tiledMap.RenderOrder.IndexOf("left-", StringComparison.OrdinalIgnoreCase) != -1
                ? -1
                : 1;
            var tilesetCache = new TilesetCache(tiledMap.Tilesets);

            var layers = tiledMap
                .Layers
                .Select(tiledMapLayer =>
                {
                    var mapLayerTiles = CreateTilesForLayer(
                        tilesetCache,
                        tiledMapLayer,
                        flipX,
                        flipY);
                    var mapLayer = new MapLayer(
                        tiledMapLayer.Name,
                        mapLayerTiles);
                    return mapLayer;
                });

            var behaviors = new List<IBehavior>()
            {
                new MapPropertiesBehavior(tiledMap.Properties),
            };

            if (tiledMap.Properties.ContainsKey("IgnoreSavingGameObjectState"))
            {
                behaviors.Add(new IgnoreSavingGameObjectStateBehavior());
            }

            var map = _mapFactory.Create(
                mapId,
                layers,
                behaviors);
            return map;
        }

        private IEnumerable<IMapTile> CreateTilesForLayer(
            ITilesetCache tilesetCache,
            Tiled.Net.Layers.IMapLayer tiledMapLayer,
            int flipX,
            int flipY)
        {
            for (var x = 0; x < tiledMapLayer.Width; x++)
            {
                for (var y = 0; y < tiledMapLayer.Height; y++)
                {
                    var tiledTile = tiledMapLayer.GetTile(x, y);
                    if (tiledTile.Gid == 0)
                    {
                        continue;
                    }

                    var tileset = tilesetCache.ForGid(tiledTile.Gid);
                    var tilesetTile = tileset.Tiles[tiledTile.Gid - 1];

                    var tilesetResourcePath = _tilesetSpriteResourceResolver.ResolveResourcePath(tileset.Images.Single().SourcePath);
                    var spriteResourceName = $"{Path.GetFileNameWithoutExtension(tilesetResourcePath)}_{tiledTile.Gid - tileset.FirstGid}";

                    var tileComponents = tilesetTile
                        .Properties
                        .Select(prop => (ITileComponent)new KeyValuePairTileComponent(
                            prop.Key,
                            prop.Value))
                        .AppendSingle(new TileResourceComponent(tilesetResourcePath, spriteResourceName));

                    var mapTile = new MapTile(
                        x * flipX,
                        y * flipY,
                        tileComponents);
                    yield return mapTile;
                }
            }
        }
    }
}