using System.Threading.Tasks;

namespace Macerus.Plugins.Features.DataPersistence.Kvp
{
    public interface IKvpDataPersistenceWriter
    {
        Task WriteAsync(IKvpDataStoreWriter writer);
    }
}
