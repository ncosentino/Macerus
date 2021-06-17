using System;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.DataPersistence
{
    public static class IDataStoreManagerExtensions
    {
        public static async Task<IDataStore> OpenOrCreateAsync(
            this IDataStoreManager dataStoreManager,
            IIdentifier id)
        {
            var dataStore = await dataStoreManager.ExistsAsync(id)
                ? await dataStoreManager.OpenAsync(id)
                : await dataStoreManager.CreateNewAsync(id);
            return dataStore;
        }
    }
}
