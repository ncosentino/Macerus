using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class SocketFilterComponent : IFilterComponent
    {

        public SocketFilterComponent(IEnumerable<KeyValuePair<IIdentifier, Tuple<int, int>>> socketRanges)
            : this(socketRanges, int.MaxValue)
        {
        }

        public SocketFilterComponent(
            IEnumerable<KeyValuePair<IIdentifier, Tuple<int, int>>> socketRanges,
            int maximumSockets)
        {
            SocketRanges = socketRanges.ToDictionary();
            MaximumSockets = maximumSockets;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = Enumerable.Empty<IFilterAttribute>();
        
        public IReadOnlyDictionary<IIdentifier, Tuple<int, int>> SocketRanges { get; }
        
        public int MaximumSockets { get; }
    }
}
