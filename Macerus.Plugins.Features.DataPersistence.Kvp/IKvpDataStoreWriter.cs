using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.DataPersistence.Kvp
{
    public interface IKvpDataStoreWriter
    {
        Task WriteAsync(IIdentifier identifier, object obj);
    }
}
