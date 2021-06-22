using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.DataPersistence.Kvp;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Mapping.Default.DataPersistence
{
    public sealed class MapStateKvpDataPersistenceHandler : 
        IDiscoverableKvpDataPersistenceWriter,
        IDiscoverableKvpDataPersistenceReader
    {
        private readonly IMapStateRepository _mapStateRepository;

        public MapStateKvpDataPersistenceHandler(IMapStateRepository mapStateRepository)
        {
            _mapStateRepository = mapStateRepository;
        }

        public async Task ReadAsync(IKvpDataStoreReader reader)
        {
            var mapStateKeys = reader
                .GetKeys()
                .Select(x =>
                {
                    if (!(x is StringIdentifier strId))
                    {
                        return null;
                    }
                    
                    if (!strId.Identifier.StartsWith("MapState/", StringComparison.Ordinal))
                    {
                        return null;
                    }

                    return new
                    {
                        Key = x,
                        MapId = new StringIdentifier(strId.Identifier.Substring("MapState/".Length)),
                    };
                })
                .Where(x => x != null);
            foreach (var mapStateKey in mapStateKeys)
            {
                var gameObjectIds = await reader.ReadAsync<IReadOnlyCollection<IIdentifier>>(mapStateKey.Key);
                _mapStateRepository.SaveState(
                    mapStateKey.MapId,
                    gameObjectIds);
            }
        }

        public async Task WriteAsync(IKvpDataStoreWriter writer)
        {
            foreach (var mapStateKvp in _mapStateRepository.GetAllState())
            {
                await writer.WriteAsync(
                    new StringIdentifier($"MapState/{mapStateKvp.Key}"),
                    mapStateKvp.Value);
            }
        }
    }
}
