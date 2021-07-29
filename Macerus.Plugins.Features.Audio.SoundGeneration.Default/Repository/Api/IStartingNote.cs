using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Api
{
    public interface IStartingNote
    {
        Channel Channel { get; }

        IWaveInstruction Note { get; }
    }
}
