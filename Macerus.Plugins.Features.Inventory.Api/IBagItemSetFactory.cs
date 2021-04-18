using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IBagItemSetFactory
    {
        IItemSet Create(IItemContainerBehavior itemContainerBehavior);
    }
}
