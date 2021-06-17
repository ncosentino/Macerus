using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.DataPersistence.Kvp.InMemory
{
    public sealed class InMemoryKvpDataStore : IKvpDataStore
    {
        private readonly Dictionary<IIdentifier, object> _cache;

        public InMemoryKvpDataStore()
        {
            _cache = new Dictionary<IIdentifier, object>();
        }

        public IEnumerable<IIdentifier> GetKeys() => _cache.Keys;

        public async Task<object> ReadAsync(IIdentifier identifier)
        {
            if (_cache.TryGetValue(
                identifier,
                out var result))
            {
                return result;
            }

            throw new KeyNotFoundException(
                $"Could not find entry for '{identifier}'.");
        }

        public async Task WriteAsync(IIdentifier identifier, object obj)
        {
            _cache.Add(identifier, obj);
        }
    }
}
