using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api
{
    public interface ISoundPatternTransformation
    {
        IEnumerable<IWaveInstruction> Transform(
            IEnumerable<IWaveInstruction> instructions);
    }
}
