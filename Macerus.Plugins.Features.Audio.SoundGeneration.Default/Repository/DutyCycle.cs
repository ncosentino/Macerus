using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository
{
    public sealed class DutyCycle : IDutyCycle
    {
        public DutyCycle(
            Channel channel,
            IWaveInstruction cycle)
        {
            Channel = channel;
            Cycle = cycle;
        }
        
        public Channel Channel { get; }

        public IWaveInstruction Cycle { get; }
    }
}
