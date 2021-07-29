using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine
{
    public sealed class WaveChannelDefinition : IWaveChannelDefinition
    {
        public WaveChannelDefinition(
            Channel type,
            IEnumerable<IWaveInstruction> instructions)
        {
            Type = type;
            Instructions = instructions.ToArray();
        }

        public Channel Type { get; }

        public IReadOnlyCollection<IWaveInstruction> Instructions { get; }
    }

}
