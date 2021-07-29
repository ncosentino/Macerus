using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine
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
