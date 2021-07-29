using System.Collections.Generic;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api
{
    public interface IWaveDefinition
    {
        IReadOnlyCollection<IWaveChannelDefinition> Channels { get; }
    }
}
