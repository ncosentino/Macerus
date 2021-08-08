using System;
using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.Encounters.Default.StartHandlers
{
    public sealed class EncounterStartLoadOrder : IEncounterStartLoaderOrder
    {
        private readonly IReadOnlyDictionary<Type, int> _ordering;

        public EncounterStartLoadOrder(IEnumerable<KeyValuePair<Type, int>> ordering)
        {
            _ordering = ordering.ToDictionary(x => x.Key, x => x.Value);
        }

        public int GetOrder(IStartEncounterHandler handler) => _ordering.TryGetValue(handler.GetType(), out var order)
            ? order
            : int.MaxValue;
    }
}
