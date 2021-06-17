namespace Macerus.Plugins.Features.DataPersistence.Kvp
{
    public interface IKvpDataStore :
        IDataStore,
        IKvpDataStoreReader,
        IKvpDataStoreWriter
    {
    }
}
