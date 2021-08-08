using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Encounters.EndHandlers;

namespace Macerus.Plugins.Features.Encounters.Default.EndHandlers
{
    public sealed class EncounterEndLoadOrder : IEncounterEndLoaderOrder
    {
        private readonly IReadOnlyDictionary<Type, int> _ordering;

        public EncounterEndLoadOrder(IEnumerable<KeyValuePair<Type, int>> ordering)
        {
            _ordering = ordering.ToDictionary(x => x.Key, x => x.Value);
        }

        public int GetOrder(IEndEncounterHandler handler) => _ordering.TryGetValue(handler.GetType(), out var order)
            ? order
            : int.MaxValue;
    }
}
