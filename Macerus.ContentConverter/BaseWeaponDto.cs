namespace Macerus.ContentConverter
{
    public sealed class BaseWeaponDto
    {
        public BaseWeaponDto(
            string itemId, 
            string itemNameStringResource, 
            string itemNameStringResourceId, 
            string itemIconResource, 
            string itemIconResourceId, 
            string itemEquipSlotName, 
            string itemEquipSlotId, 
            int itemLevel, 
            double itemAttackSpeed, 
            double itemRange, 
            int itemLevelRequirement, 
            int itemStrengthRequirement, 
            int itemDexterityRequirement, 
            int itemIntelligenceRequirement, 
            int itemSpeedRequirement, 
            int itemVitalityRequirement, 
            int itemPhysicalDamageMinMin, 
            int itemPhysicalDamageMinMax, 
            int itemPhysicalDamageMaxMin, 
            int itemPhysicalDamageMaxMax, 
            int itemMagicDamageMinMin, 
            int itemMagicDamageMinMax, 
            int itemMagicDamageMaxMin, 
            int itemMagicDamageMaxMax, 
            int itemDurabilityMinimum, 
            int itemDurabilityMaximum, 
            int itemSocketsMinimum, 
            int itemSocketsMaximum)
        {
            ItemId = itemId;
            ItemNameStringResource = itemNameStringResource;
            ItemNameStringResourceId = itemNameStringResourceId;
            ItemIconResource = itemIconResource;
            ItemIconResourceId = itemIconResourceId;
            ItemEquipSlotName = itemEquipSlotName;
            ItemEquipSlotId = itemEquipSlotId;
            ItemLevel = itemLevel;
            ItemAttackSpeed = itemAttackSpeed;
            ItemRange = itemRange;
            ItemLevelRequirement = itemLevelRequirement;
            ItemStrengthRequirement = itemStrengthRequirement;
            ItemDexterityRequirement = itemDexterityRequirement;
            ItemIntelligenceRequirement = itemIntelligenceRequirement;
            ItemSpeedRequirement = itemSpeedRequirement;
            ItemVitalityRequirement = itemVitalityRequirement;
            ItemPhysicalDamageMinMin = itemPhysicalDamageMinMin;
            ItemPhysicalDamageMinMax = itemPhysicalDamageMinMax;
            ItemPhysicalDamageMaxMin = itemPhysicalDamageMaxMin;
            ItemPhysicalDamageMaxMax = itemPhysicalDamageMaxMax;
            ItemMagicDamageMinMin = itemMagicDamageMinMin;
            ItemMagicDamageMinMax = itemMagicDamageMinMax;
            ItemMagicDamageMaxMin = itemMagicDamageMaxMin;
            ItemMagicDamageMaxMax = itemMagicDamageMaxMax;
            ItemDurabilityMinimum = itemDurabilityMinimum;
            ItemDurabilityMaximum = itemDurabilityMaximum;
            ItemSocketsMinimum = itemSocketsMinimum;
            ItemSocketsMaximum = itemSocketsMaximum;
        }

        public string ItemId { get; }

        public string ItemNameStringResource { get; }
        public string ItemNameStringResourceId { get; }

        public string ItemIconResource { get; }
        public string ItemIconResourceId { get; }

        public string ItemEquipSlotName { get; }
        public string ItemEquipSlotId { get; }

        public int ItemLevel { get; }

        public double ItemAttackSpeed { get; }
        public double ItemRange { get; }

        public int ItemLevelRequirement { get; }
        public int ItemStrengthRequirement { get; }
        public int ItemDexterityRequirement { get; }
        public int ItemIntelligenceRequirement { get; }
        public int ItemSpeedRequirement { get; }
        public int ItemVitalityRequirement { get; }

        public int ItemPhysicalDamageMinMin { get; }
        public int ItemPhysicalDamageMinMax { get; }
        public int ItemPhysicalDamageMaxMin { get; }
        public int ItemPhysicalDamageMaxMax { get; }
        public int ItemMagicDamageMinMin { get; }
        public int ItemMagicDamageMinMax { get; }
        public int ItemMagicDamageMaxMin { get; }
        public int ItemMagicDamageMaxMax { get; }

        public int ItemDurabilityMinimum { get; }
        public int ItemDurabilityMaximum { get; }

        public int ItemSocketsMinimum { get; }
        public int ItemSocketsMaximum { get; }
    }
}
