using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository
{
    public sealed class StartingNote : IStartingNote
    {
        public StartingNote(
            Channel channel,
            IWaveInstruction note)
        {
            Channel = channel;
            Note = note;

        }

        public Channel Channel { get; }

        public IWaveInstruction Note { get; }
    }
}
