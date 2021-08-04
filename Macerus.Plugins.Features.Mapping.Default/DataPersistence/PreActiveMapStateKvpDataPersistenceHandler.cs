using System.Threading.Tasks;

using Macerus.Plugins.Features.DataPersistence.Kvp;

using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Mapping.Default.DataPersistence
{
    public sealed class PreActiveMapStateKvpDataPersistenceHandler :
        IDiscoverableKvpDataPersistenceWriter,
        IDiscoverableKvpDataPersistenceReader
    {
        private readonly IMapManager _mapManager;

        public PreActiveMapStateKvpDataPersistenceHandler(IMapManager mapManager)
        {
            _mapManager = mapManager;
        }

        public async Task WriteAsync(IKvpDataStoreWriter writer)
        {
            await _mapManager
                .SaveActiveMapStateAsync()
                .ConfigureAwait(false);
        }

        public async Task ReadAsync(IKvpDataStoreReader reader)
        {
            await _mapManager
                .UnloadMapAsync()
                .ConfigureAwait(false);
        }
    }
}
