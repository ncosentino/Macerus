using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.DataPersistence
{
    public interface IDataStoreManager
    {
        Task<bool> ExistsAsync(IIdentifier id);

        Task<IDataStore> CreateNewAsync(IIdentifier id);

        Task<IDataStore> OpenAsync(IIdentifier id);

        IEnumerable<IIdentifier> GetExistingDataStoreIds();
    }
}
