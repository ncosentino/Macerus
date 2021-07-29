using System.Collections.Generic;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api
{
    public interface IWaveChannelDefinition
    {
        Channel Type { get; }

        IReadOnlyCollection<IWaveInstruction> Instructions {get;}
    }

}
