using System.Threading.Tasks;

namespace Macerus.Plugins.Features.DataPersistence
{
    public interface IDataPersistenceHandler
    {
        Task WriteAsync(IDataStore dataStore);

        Task ReadAsync(IDataStore dataStore);
    }
}
