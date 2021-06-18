using System.Threading.Tasks;

using Macerus.Plugins.Features.DataPersistence.Kvp;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Mapping.Default.DataPersistence
{
    public sealed class PostActiveMapStateKvpDataPersistenceHandler :
        IDiscoverableKvpDataPersistenceWriter,
        IDiscoverableKvpDataPersistenceReader
    {
        private readonly IMapManager _mapManager;

        public PostActiveMapStateKvpDataPersistenceHandler(IMapManager mapManager)
        {
            _mapManager = mapManager;
        }

        public async Task WriteAsync(IKvpDataStoreWriter writer)
        {
            await writer.WriteAsync(
                new StringIdentifier("ActiveMapId"),
                _mapManager.ActiveMap.GetOnly<IReadOnlyIdentifierBehavior>().Id);
        }

        public async Task ReadAsync(IKvpDataStoreReader reader)
        {
            var mapId = (IIdentifier)await reader.ReadAsync(new StringIdentifier("ActiveMapId"));
            await _mapManager.SwitchMapAsync(mapId);
        }
    }
}
