using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework.Collections;

namespace Macerus.Plugins.Features.Mapping.Default
{
    public sealed class MapRepository : IDiscoverableMapRepository
    {
        private readonly IMapIdentifiers _mapIdentifiers;
        private readonly IDeserializer _deserializer;
        private readonly IMapResourceIdConverter _mapResourceIdConverter;
        private readonly IMapResourceLoader _resourceLoader;
        private readonly ICache<IIdentifier, IGameObject> _mapCache;

        public MapRepository(
            IMapIdentifiers mapIdentifiers,
            IDeserializer deserializer,
            IMapResourceIdConverter mapResourceIdConverter,
            IMapResourceLoader resourceLoader)
        {
            _mapIdentifiers = mapIdentifiers;
            _deserializer = deserializer;
            _mapResourceIdConverter = mapResourceIdConverter;
            _resourceLoader = resourceLoader;
            _mapCache = new Cache<IIdentifier, IGameObject>(5);
        }

        public IEnumerable<IGameObject> LoadMaps(IFilterContext filterContext)
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

            IGameObject cached;
            if (_mapCache.TryGetValue(mapId, out cached))
            {
                yield return cached;
                yield break;
            }

            var mapResourcePath = _mapResourceIdConverter.ConvertToMapResourcePath(mapId.ToString());

            IGameObject map;
            using (var mapResourceStream = _resourceLoader.LoadStream(mapResourcePath))
            {
                // FIXME: we need this while we support Tiled in parallel
                if (mapResourceStream == null)
                {
                    _mapCache.AddOrUpdate(mapId, null);
                    yield break;
                }

                map = _deserializer.Deserialize<IGameObject>(mapResourceStream);
            }

            _mapCache.AddOrUpdate(mapId, map);
            yield return map;
        }
    }
}
