using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using NexusLabs.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine
{
    public sealed class WaveDefinition : IWaveDefinition
    {
        public WaveDefinition(IEnumerable<IWaveChannelDefinition> channels)
        {
            Channels = channels.ToArray();
        }

        public IReadOnlyCollection<IWaveChannelDefinition> Channels { get; }
    }
}
