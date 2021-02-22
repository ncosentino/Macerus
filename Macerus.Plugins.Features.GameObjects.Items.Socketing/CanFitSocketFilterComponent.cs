using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class CanFitSocketFilterComponent : IFilterComponent
    {
        public CanFitSocketFilterComponent(IIdentifier socketId, int size)
        {
            SocketId = socketId;
            Size = size;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = Enumerable.Empty<IFilterAttribute>();
        
        public IIdentifier SocketId { get; }
        
        public int Size { get; }
    }
}
