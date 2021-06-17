using System.Threading.Tasks;

using Macerus.Plugins.Features.DataPersistence.Kvp;

using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Mapping.Default.DataPersistence
{
    public sealed class MapStateKvpDataPersistenceHandler : IDiscoverableKvpDataPersistenceWriter
    {
        private readonly IMapStateRepository _mapStateRepository;

        public MapStateKvpDataPersistenceHandler(IMapStateRepository mapStateRepository)
        {
            _mapStateRepository = mapStateRepository;
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
