using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public sealed class TiledNetMapRepository : IDiscoverableMapRepository
    {
        private readonly IMapIdentifiers _mapIdentifiers;
        private readonly ITiledNetToMapConverter _tiledNetToMapConverter;
        private readonly ITiledMapLoader _tiledMapLoader;
        private readonly ICache<IIdentifier, IMap> _mapCache;

        public TiledNetMapRepository(
            IMapIdentifiers mapIdentifiers,
            ITiledNetToMapConverter tiledNetToMapConverter,
            ITiledMapLoader tiledMapLoader,
            ICache<IIdentifier, IMap> mapCache)
        {
            _mapIdentifiers = mapIdentifiers;
            _tiledNetToMapConverter = tiledNetToMapConverter;
            _tiledMapLoader = tiledMapLoader;
            _mapCache = mapCache;
        }

        public IEnumerable<IMap> LoadMaps(IFilterContext filterContext)
        {
            var requiredAttributes = filterContext
                    .Attributes
                    .Where(x => x.Required)
                    .ToArray();
            if (requiredAttributes.Length != 1 ||
                !requiredAttributes.Single().Id.Equals(_mapIdentifiers.FilterContextMapIdentifier) ||
                !(requiredAttributes.Single().Value is IdentifierFilterAttributeValue))
            {
                throw new NotSupportedException(
                    "// FIXME: This is the API we want to encourage, but currently there " +
                    "is only support for loading maps by ID. You could be the " +
                    "hero we need. Implement filtering!");
            }

            var mapId = ((IdentifierFilterAttributeValue)requiredAttributes.Single().Value).Value;

            IMap cached;
            if (_mapCache.TryGetValue(mapId, out cached))
            {
                yield return cached;
                yield break;
            }

            var tiledMap = _tiledMapLoader.LoadMap(mapId);

            var map = _tiledNetToMapConverter.Convert(
                mapId,
                tiledMap);
            _mapCache.AddOrUpdate(mapId, map);

            yield return map;
        }
    }
}