namespace Macerus.Plugins.Features.DataPersistence
{
    public interface IDiscoverableDataPersistenceHandler : IDataPersistenceHandler
    {
        bool CanRead(IDataStore dataStore);

        bool CanWrite(IDataStore dataStore);
    }
}
