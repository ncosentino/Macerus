using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Api
{
    public interface IDutyCycle
    {
        Channel Channel { get; }

        IWaveInstruction Cycle { get; }
    }
}
