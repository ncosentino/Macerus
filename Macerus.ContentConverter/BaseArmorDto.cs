using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class BaseArmorDto
    {
        public BaseArmorDto(
            string itemId, 
            string itemNameStringResource, 
            string itemNameStringResourceId, 
            string itemIconResource, 
            string itemIconResourceId, 
            string itemEquipSlotName, 
            string itemEquipSlotId, 
            int itemLevel, 
            double itemBlock, 
            int itemLevelRequirement, 
            int itemStrengthRequirement, 
            int itemDexterityRequirement, 
            int itemIntelligenceRequirement, 
            int itemSpeedRequirement, 
            int itemVitalityRequirement,
            int itemArmorMinimum,
            int itemArmorMaximum,
            int itemEvasionMinimum,
            int itemEvasionMaximum,
            int itemDurabilityMinimum, 
            int itemDurabilityMaximum, 
            int itemSocketsMinimum, 
            int itemSocketsMaximum,
            IReadOnlyCollection<string> tags)
        {
            ItemId = itemId;
            ItemNameStringResource = itemNameStringResource;
            ItemNameStringResourceId = itemNameStringResourceId;
            ItemIconResource = itemIconResource;
            ItemIconResourceId = itemIconResourceId;
            ItemEquipSlotName = itemEquipSlotName;
            ItemEquipSlotId = itemEquipSlotId;
            ItemLevel = itemLevel;
            ItemBlock = itemBlock;
            ItemLevelRequirement = itemLevelRequirement;
            ItemStrengthRequirement = itemStrengthRequirement;
            ItemDexterityRequirement = itemDexterityRequirement;
            ItemIntelligenceRequirement = itemIntelligenceRequirement;
            ItemSpeedRequirement = itemSpeedRequirement;
            ItemVitalityRequirement = itemVitalityRequirement;
            ItemArmorMinimum = itemArmorMinimum;
            ItemArmorMaximum = itemArmorMaximum;
            ItemEvasionMinimum = itemEvasionMinimum;
            ItemEvasionMaximum = itemEvasionMaximum;
            ItemDurabilityMinimum = itemDurabilityMinimum;
            ItemDurabilityMaximum = itemDurabilityMaximum;
            ItemSocketsMinimum = itemSocketsMinimum;
            ItemSocketsMaximum = itemSocketsMaximum;
            Tags = tags;
        }

        public string ItemId { get; }

        public string ItemNameStringResource { get; }
        public string ItemNameStringResourceId { get; }

        public string ItemIconResource { get; }
        public string ItemIconResourceId { get; }

        public string ItemEquipSlotName { get; }
        public string ItemEquipSlotId { get; }

        public int ItemLevel { get; }

        public double ItemBlock { get; }

        public int ItemLevelRequirement { get; }
        public int ItemStrengthRequirement { get; }
        public int ItemDexterityRequirement { get; }
        public int ItemIntelligenceRequirement { get; }
        public int ItemSpeedRequirement { get; }
        public int ItemVitalityRequirement { get; }
        public int ItemArmorMinimum { get; }
        public int ItemArmorMaximum { get; }
        public int ItemEvasionMinimum { get; }
        public int ItemEvasionMaximum { get; }
        public int ItemDurabilityMinimum { get; }
        public int ItemDurabilityMaximum { get; }

        public int ItemSocketsMinimum { get; }
        public int ItemSocketsMaximum { get; }
        public IReadOnlyCollection<string> Tags { get; }
    }
}
