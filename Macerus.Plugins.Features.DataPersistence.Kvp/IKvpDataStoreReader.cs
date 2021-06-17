using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.DataPersistence.Kvp
{
    public interface IKvpDataStoreReader
    {
        Task<object> ReadAsync(IIdentifier identifier);
    }
}
