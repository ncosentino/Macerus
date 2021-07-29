using System.Collections.Generic;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Api
{
    public interface ISoundPatternTransformation
    {
        IEnumerable<IWaveInstruction> Transform(
            IEnumerable<IWaveInstruction> instructions);
    }
}
