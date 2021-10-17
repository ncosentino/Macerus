using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Inventory.Api.HoverCards;

namespace Macerus.Content.Items
{
    public sealed class HoverCardPartConverterLoadOrder : IHoverCardPartConverterLoadOrder
    {
        private readonly IReadOnlyDictionary<Type, int> _ordering;

        public HoverCardPartConverterLoadOrder(IEnumerable<KeyValuePair<Type, int>> ordering)
        {
            _ordering = ordering.ToDictionary(x => x.Key, x => x.Value);
        }

        public int GetOrder(IBehaviorsToHoverCardPartViewModelConverter converter) =>
            _ordering.TryGetValue(converter.GetType(), out var order)
            ? order
            : int.MaxValue;
    }
}
