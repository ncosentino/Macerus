using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api
{
    public interface IWaveChannelDefinition
    {
        Channel Type { get; }

        IReadOnlyCollection<IWaveInstruction> Instructions {get;}
    }

}
