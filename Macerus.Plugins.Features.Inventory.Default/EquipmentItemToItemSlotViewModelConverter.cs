﻿using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using Macerus.Plugins.Features.Gui;
using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class EquipmentItemToItemSlotViewModelConverter : IItemToItemSlotViewModelConverter
    {
        public IItemSlotViewModel Convert(
            IIdentifier idForItemSet,
            IGameObject item)
        {
            var slotId = idForItemSet;
            var iconResourceId = item?.Get<IHasInventoryIcon>().FirstOrDefault()?.IconResourceId;

            var inventoryIconColorBehavior = item?.Get<IHasInventoryIconColor>().FirstOrDefault();
            var iconOpacity = inventoryIconColorBehavior == null ? 1 : inventoryIconColorBehavior.IconOpacity;
            var iconColor = inventoryIconColorBehavior == null ? (IColor)null : new Color()
            {
                A = inventoryIconColorBehavior.A,
                R = inventoryIconColorBehavior.R,
                G = inventoryIconColorBehavior.G,
                B = inventoryIconColorBehavior.B,
            };
            var inventorySlotBackgroundColorBehavior = item?.Get<IHasInventoryBackgroundColor>().FirstOrDefault();
            var slotBackgroundColor = inventorySlotBackgroundColorBehavior == null ? (IColor)null : new Color()
            {
                R = inventorySlotBackgroundColorBehavior.R,
                G = inventorySlotBackgroundColorBehavior.G,
                B = inventorySlotBackgroundColorBehavior.B,
            };
            var itemSlotViewModel = new ItemSlotViewModel(
                ConvertBackendToViewModelId(slotId),
                item != null,
                iconResourceId,
                iconOpacity,
                iconColor,
                slotBackgroundColor,
                slotId.ToString());
            return itemSlotViewModel;
        }

        private object ConvertBackendToViewModelId(IIdentifier id)
        {
            return id.ToString();
        }
    }
}