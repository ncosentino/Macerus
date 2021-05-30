
using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class BagItemSetFactory : IBagItemSetFactory
    {
        private readonly ISocketPatternHandlerFacade _socketPatternHandler;
        private readonly ISocketableInfoFactory _socketableInfoFactory;

        public BagItemSetFactory(
            ISocketPatternHandlerFacade socketPatternHandler,
            ISocketableInfoFactory socketableInfoFactory)
        {
            _socketPatternHandler = socketPatternHandler;
            _socketableInfoFactory = socketableInfoFactory;
        }

        public IItemSet Create(IItemContainerBehavior itemContainerBehavior)
        {
            var bagItemSet = new BagItemSet(
                itemContainerBehavior,
                _socketPatternHandler,
                _socketableInfoFactory);
            return bagItemSet;
        }
    }
}
