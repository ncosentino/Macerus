using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.DataPersistence.Kvp;

namespace Macerus.Plugins.Content.DataPersistence.Kvp
{
    public sealed class KvpDataPersistenceHandlerLoadOrder : IKvpDataPersistenceHandlerLoadOrder
    {
        private readonly IReadOnlyDictionary<Type, int> _ordering;

        public KvpDataPersistenceHandlerLoadOrder(IEnumerable<KeyValuePair<Type, int>> ordering)
        {
            _ordering = ordering.ToDictionary(x => x.Key, x => x.Value);
        }

        public int GetOrder(IKvpDataPersistenceWriter writer) => 
            _ordering.TryGetValue(writer.GetType(), out var order)
            ? order
            : int.MaxValue;

        public int GetOrder(IKvpDataPersistenceReader reader) =>
            _ordering.TryGetValue(reader.GetType(), out var order)
            ? order
            : int.MaxValue;
    }
}
