using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api
{
    public interface IChannelSpecification
    {
        Channel Channel { get; }

        IReadOnlyCollection<IOpSpecification> OpSpecifications { get; }
    }
}
