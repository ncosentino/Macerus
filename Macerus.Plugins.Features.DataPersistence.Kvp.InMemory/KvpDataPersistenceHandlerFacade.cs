using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.DataPersistence.Kvp.InMemory
{
    /// <summary>
    /// NOTE: this is not coupled to an in-memory implementation and could be moved out if needed
    /// </summary>
    public sealed class KvpDataPersistenceHandlerFacade : IDiscoverableDataPersistenceHandler
    {
        private readonly IReadOnlyCollection<IDiscoverableKvpDataPersistenceWriter> _writers;
        private readonly IReadOnlyCollection<IDiscoverableKvpDataPersistenceReader> _readers;

        public KvpDataPersistenceHandlerFacade(
            IKvpDataPersistenceHandlerLoadOrder loadOrder,
            IEnumerable<IDiscoverableKvpDataPersistenceWriter> writers,
            IEnumerable<IDiscoverableKvpDataPersistenceReader> readers)
        {
            _writers = writers
                .OrderBy(x => loadOrder.GetOrder(x))
                .ToArray();
            _readers = readers
                .OrderBy(x => loadOrder.GetOrder(x))
                .ToArray();
        }

        public bool CanRead(IDataStore dataStore) => dataStore is IKvpDataStoreReader;

        public bool CanWrite(IDataStore dataStore) => dataStore is IKvpDataStoreWriter;

        public async Task WriteAsync(IDataStore dataStore)
        {
            var storeWriter = (IKvpDataStoreWriter)dataStore;

            foreach (var wrapped in _writers)
            {
                await wrapped.WriteAsync(storeWriter);
            }
        }

        public async Task ReadAsync(IDataStore dataStore)
        {
            var storeReader = (IKvpDataStoreReader)dataStore;

            foreach (var wrapped in _readers)
            {
                await wrapped.ReadAsync(storeReader);
            }
        }
    }
}
