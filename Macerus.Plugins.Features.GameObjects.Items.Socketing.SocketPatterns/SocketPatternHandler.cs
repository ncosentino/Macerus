using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Socketing.SocketPatterns
{
    public sealed class SocketPatternHandler : IDiscoverableSocketPatternHandler
    {
        public bool TryHandle(
            ISocketableInfo socketableInfo,
            out IGameObject newItem)
        {
            newItem = null;
            return false;
        }
    }
}
