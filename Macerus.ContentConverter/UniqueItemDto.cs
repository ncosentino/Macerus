using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class UniqueItemDto
    {
        public UniqueItemDto(
            string uniqueItemId,
            string baseItemId, 
            string itemNameStringResourceId, 
            string itemIconStringResourceId, 
            IReadOnlyCollection<string> enchantmentDefinitionIds)
        {
            UniqueItemId = uniqueItemId;
            BaseItemId = baseItemId;
            ItemNameStringResourceId = itemNameStringResourceId;
            ItemIconStringResourceId = itemIconStringResourceId;
            EnchantmentDefinitionIds = enchantmentDefinitionIds;
        }

        public string UniqueItemId { get; }

        public string BaseItemId { get; }

        public string ItemNameStringResourceId { get; }

        public string ItemIconStringResourceId { get; }

        public IReadOnlyCollection<string> EnchantmentDefinitionIds { get; }
    }
}
