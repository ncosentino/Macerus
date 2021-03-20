using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;

using Tiled.Net.Maps;
using Tiled.Net.Parsers;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public sealed class CachingTiledMapLoader : ITiledMapLoader
    {
        private readonly ITiledMapResourceLoader _resourceLoader;
        private readonly IMapParser _mapParser;
        private readonly IMapResourceIdConverter _mapResourceIdConverter;
        private readonly ICache<IIdentifier, ITiledMap> _tiledMapCache;

        public CachingTiledMapLoader(
            ITiledMapResourceLoader resourceLoader,
            IMapParser mapParser,
            IMapResourceIdConverter mapResourceIdConverter,
            ICache<IIdentifier, ITiledMap> tiledMapCache)
        {
            _resourceLoader = resourceLoader;
            _mapParser = mapParser;
            _mapResourceIdConverter = mapResourceIdConverter;
            _tiledMapCache = tiledMapCache;
        }

        public ITiledMap LoadMap(IIdentifier mapId)
        {
            ITiledMap cached;
            if (_tiledMapCache.TryGetValue(mapId, out cached))
            {
                return cached;
            }

            var mapResourcePath = _mapResourceIdConverter.Convert(mapId.ToString());

            ITiledMap tiledMap;
            using (var mapResourceStream = _resourceLoader.LoadStream(mapResourcePath))
            {
                tiledMap = _mapParser.ParseMap(mapResourceStream);
            }

            _tiledMapCache.AddOrUpdate(mapId, tiledMap);
            return tiledMap;
        }
    }
}