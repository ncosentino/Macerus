using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class SocketGeneratorComponent : IGeneratorComponent
    {
        public SocketGeneratorComponent(IEnumerable<KeyValuePair<IIdentifier, Tuple<int, int>>> socketRanges)
            : this(socketRanges, int.MaxValue)
        {
        }

        public SocketGeneratorComponent(
            IEnumerable<KeyValuePair<IIdentifier, Tuple<int, int>>> socketRanges,
            int maximumSockets)
        {
            SocketRanges = socketRanges.ToDictionary();
            MaximumSockets = maximumSockets;
        }
        
        public IReadOnlyDictionary<IIdentifier, Tuple<int, int>> SocketRanges { get; }
        
        public int MaximumSockets { get; }
    }
}
