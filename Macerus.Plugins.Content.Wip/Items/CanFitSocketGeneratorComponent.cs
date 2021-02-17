using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class CanFitSocketGeneratorComponent : IGeneratorComponent
    {
        public CanFitSocketGeneratorComponent(IIdentifier socketId, int size)
        {
            SocketId = socketId;
            Size = size;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = Enumerable.Empty<IGeneratorAttribute>();
        
        public IIdentifier SocketId { get; }
        
        public int Size { get; }
    }
}
