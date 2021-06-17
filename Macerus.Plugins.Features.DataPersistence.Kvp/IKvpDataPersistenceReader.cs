using System.Threading.Tasks;

namespace Macerus.Plugins.Features.DataPersistence.Kvp
{
    public interface IKvpDataPersistenceReader
    {
        Task ReadAsync(IKvpDataStoreReader reader);
    }
}
