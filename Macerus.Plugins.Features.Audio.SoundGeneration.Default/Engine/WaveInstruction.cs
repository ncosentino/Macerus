using System.Linq;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine
{
    public sealed class WaveInstruction : IWaveInstruction
    {
        public WaveInstruction(params int[] ops)
        {
            Ops = ops.ToArray();
        }

        public int[] Ops { get; }
    }
}
