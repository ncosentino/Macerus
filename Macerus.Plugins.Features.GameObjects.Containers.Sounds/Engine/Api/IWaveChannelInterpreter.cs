using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api
{
    public interface IWaveChannelInterpreter
    {
        IChannelSpecification ChannelSpecification { get; }

        long FindWaveLength(IWaveChannelDefinition waveDefinition, int length, IReadOnlyCollection<double> wave);

        IReadOnlyCollection<double> Interpret(IWaveChannelDefinition definition, int pitch, int length, int cutoff);

    }
}
