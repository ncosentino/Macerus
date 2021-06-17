namespace Macerus.Plugins.Features.DataPersistence.Kvp.InMemory
{
    public sealed class NoneHandlerLoadOrder : IKvpDataPersistenceHandlerLoadOrder
    {
        public int GetOrder(IKvpDataPersistenceWriter writer) => int.MaxValue;

        public int GetOrder(IKvpDataPersistenceReader reader) => int.MaxValue;
    }
}
