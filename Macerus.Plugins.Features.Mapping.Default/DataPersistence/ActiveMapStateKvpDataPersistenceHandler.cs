using System.Threading.Tasks;

using Macerus.Plugins.Features.DataPersistence.Kvp;

using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Mapping.Default.DataPersistence
{
    public sealed class ActiveMapStateKvpDataPersistenceHandler :
        IDiscoverableKvpDataPersistenceWriter,
        IDiscoverableKvpDataPersistenceReader
    {
        private readonly IMapManager _mapManager;

        public ActiveMapStateKvpDataPersistenceHandler(IMapManager mapManager)
        {
            _mapManager = mapManager;
        }

        public async Task WriteAsync(IKvpDataStoreWriter writer)
        {
            await _mapManager.SaveActiveMapStateAsync();
        }

        public async Task ReadAsync(IKvpDataStoreReader reader)
        {
            _mapManager.UnloadMap();
        }
    }
}
