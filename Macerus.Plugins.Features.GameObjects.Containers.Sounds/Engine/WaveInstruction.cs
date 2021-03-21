using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using System.Linq;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine
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
