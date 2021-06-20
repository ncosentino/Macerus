using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.DataPersistence.Kvp.InMemory
{
    public sealed class InMemoryKvpDataStoreManager : IDataStoreManager
    {
        private readonly Dictionary<IIdentifier, IKvpDataStore> _dataStores;
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;

        public InMemoryKvpDataStoreManager(
            ISerializer serializer,
            IDeserializer deserializer)
        {
            _dataStores = new Dictionary<IIdentifier, IKvpDataStore>();
            _serializer = serializer;
            _deserializer = deserializer;
        }

        public async Task<IDataStore> CreateNewAsync(IIdentifier id)
        {
            if (_dataStores.ContainsKey(id))
            {
                throw new InvalidOperationException(
                    $"A data store with ID '{id}' already exists.");
            }

            var dataStore = new InMemoryKvpDataStore(
                _serializer,
                _deserializer);
            _dataStores[id] = dataStore;

            return dataStore;
        }

        public async Task<IDataStore> OpenAsync(IIdentifier id)
        {
            if (_dataStores.TryGetValue(
                id,
                out var result))
            {
                return result;
            }

            throw new KeyNotFoundException(
                $"Could not find data store for ID '{id}'.");
        }

        public async Task<bool> ExistsAsync(IIdentifier id) =>
            _dataStores.ContainsKey(id);

        public IEnumerable<IIdentifier> GetExistingDataStoreIds() =>
            _dataStores.Keys;
    }
}
