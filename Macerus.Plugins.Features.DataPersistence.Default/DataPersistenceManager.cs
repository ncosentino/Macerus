using System;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.DataPersistence.Default
{
    public sealed class DataPersistenceManager : IDataPersistenceManager
    {
        private readonly IDataStoreManager _dataStoreManager;
        private readonly IDataPersistenceHandlerFacade _dataPersistenceHandlerFacade;

        public DataPersistenceManager(
            IDataStoreManager dataStoreManager,
            IDataPersistenceHandlerFacade dataPersistenceHandlerFacade)
        {
            _dataStoreManager = dataStoreManager;
            _dataPersistenceHandlerFacade = dataPersistenceHandlerFacade;
        }

        public async Task SaveAsync(IIdentifier id)
        {
            var dataStore = await _dataStoreManager.OpenOrCreateAsync(id);
            await _dataPersistenceHandlerFacade.WriteAsync(dataStore);
        }

        public async Task LoadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
