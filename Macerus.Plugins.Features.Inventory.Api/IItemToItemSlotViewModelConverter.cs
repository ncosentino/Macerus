using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IItemToItemSlotViewModelConverter
    {
        IItemSlotViewModel Convert(
            IIdentifier idForItemSet,
            IGameObject item);
    }
}