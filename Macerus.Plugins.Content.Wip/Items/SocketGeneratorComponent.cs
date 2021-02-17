using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Content.Wip.Items
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

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = Enumerable.Empty<IGeneratorAttribute>();
        
        public IReadOnlyDictionary<IIdentifier, Tuple<int, int>> SocketRanges { get; }
        
        public int MaximumSockets { get; }
    }
}
