using System.Collections.Generic;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api
{
    public interface IChannelSpecification
    {
        Channel Channel { get; }

        IReadOnlyCollection<IOpSpecification> OpSpecifications { get; }
    }
}
