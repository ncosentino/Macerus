using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.DataPersistence.Kvp.InMemory
{
    public sealed class InMemoryKvpDataStore : IKvpDataStore
    {
        private readonly Dictionary<IIdentifier, string> _cache;
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;

        public InMemoryKvpDataStore(
            ISerializer serializer,
            IDeserializer deserializer)
        {
            _cache = new Dictionary<IIdentifier, string>();
            _serializer = serializer;
            _deserializer = deserializer;
        }

        public IEnumerable<IIdentifier> GetKeys() => _cache.Keys;

        public async Task<T> ReadAsync<T>(IIdentifier identifier)
        {
            if (_cache.TryGetValue(
                identifier,
                out var cached))
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(cached)))
                {
                    var result = _deserializer.Deserialize<T>(stream);
                    return result;
                }
            }

            throw new KeyNotFoundException(
                $"Could not find entry for '{identifier}'.");
        }

        public async Task WriteAsync(IIdentifier identifier, object obj)
        {
            var serialized = _serializer.SerializeToString(obj, Encoding.UTF8);
            _cache.Add(identifier, serialized);
        }
    }
}
