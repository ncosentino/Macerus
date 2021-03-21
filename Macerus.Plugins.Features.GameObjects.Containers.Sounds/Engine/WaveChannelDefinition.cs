using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine
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
