using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.DataPersistence
{
    public interface IDataPersistenceManager
    {
        Task LoadAsync();

        Task SaveAsync(IIdentifier id);
    }
}
