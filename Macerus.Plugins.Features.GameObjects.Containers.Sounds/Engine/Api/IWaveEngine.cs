using System;
using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api
{
    public interface IWaveEngine
    {
        IEngineSpecifications EngineSpecifications { get; }

        IReadOnlyCollection<IChannelSpecification> ChannelSpecifications { get; }

        double[] ConvertDefinitionToWave(IWaveDefinition definition, int pitch, int length, int volume);
    }
}
