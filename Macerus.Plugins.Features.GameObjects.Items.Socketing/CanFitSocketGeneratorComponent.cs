using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class CanFitSocketGeneratorComponent : IGeneratorComponent
    {
        public CanFitSocketGeneratorComponent(IIdentifier socketId, int size)
        {
            SocketId = socketId;
            Size = size;
        }
        
        public IIdentifier SocketId { get; }
        
        public int Size { get; }
    }
}
