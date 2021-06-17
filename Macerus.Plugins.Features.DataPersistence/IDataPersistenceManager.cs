using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.DataPersistence
{
    public interface IDataPersistenceManager
    {
        Task LoadAsync(IIdentifier id);

        Task SaveAsync(IIdentifier id);
    }
}
