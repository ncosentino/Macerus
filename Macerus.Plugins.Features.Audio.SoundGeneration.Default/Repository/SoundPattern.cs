using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository
{
    public sealed class SoundPattern : ISoundPattern
    {
        public SoundPattern(
            Channel channel,
            IEnumerable<IWaveInstruction> transforms)
        {
            Channel = channel;
            Transforms = transforms.ToArray();

        }

        public Channel Channel { get; }
        
        public IReadOnlyCollection<IWaveInstruction> Transforms { get; }
    }
}
