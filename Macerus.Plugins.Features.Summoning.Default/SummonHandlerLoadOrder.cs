using System;
using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummonHandlerLoadOrder : ISummonHandlerLoadOrder
    {
        private readonly IReadOnlyDictionary<Type, int> _ordering;

        public SummonHandlerLoadOrder(IEnumerable<KeyValuePair<Type, int>> ordering)
        {
            _ordering = ordering.ToDictionary(x => x.Key, x => x.Value);
        }

        public int GetOrder(IDiscoverableSummonHandler handler) =>
            _ordering.TryGetValue(handler.GetType(), out var order)
            ? order
            : handler.Priority.HasValue
                ? handler.Priority.Value
                : int.MaxValue;
    }
}
