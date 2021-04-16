using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class EquipmentItemToItemSlotViewModelConverter : IItemToItemSlotViewModelConverter
    {
        public IItemSlotViewModel Convert(
            IIdentifier idForItemSet,
            IGameObject item)
        {
            var slotId = idForItemSet;
            var rawIconResource = item?.Get<IHasInventoryIcon>().FirstOrDefault()?.IconResource;
            var iconResourceId = string.IsNullOrWhiteSpace(rawIconResource)
                ? null
                : new StringIdentifier(rawIconResource);
            var itemSlotViewModel = new ItemSlotViewModel(
                ConvertBackendToViewModelId(slotId),
                item != null,
                iconResourceId,
                slotId.ToString());
            return itemSlotViewModel;
        }

        private object ConvertBackendToViewModelId(IIdentifier id)
        {
            return id.ToString();
        }
    }
}