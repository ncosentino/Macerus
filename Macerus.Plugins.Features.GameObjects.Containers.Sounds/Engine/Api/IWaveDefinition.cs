using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api
{
    public interface IWaveDefinition
    {
        IReadOnlyCollection<IWaveChannelDefinition> Channels { get; }
    }
}
