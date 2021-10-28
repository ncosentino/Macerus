using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class UniqueItemDto
    {
        public UniqueItemDto(
            string uniqueItemId,
            string baseItemId, 
            string itemNameStringResourceId,
            ImageResourceDto itemIconResourceDto, 
            IReadOnlyCollection<string> enchantmentDefinitionIds)
        {
            UniqueItemId = uniqueItemId;
            BaseItemId = baseItemId;
            ItemNameStringResourceId = itemNameStringResourceId;
            ItemIconResourceDto = itemIconResourceDto;
            EnchantmentDefinitionIds = enchantmentDefinitionIds;
        }

        public string UniqueItemId { get; }

        public string BaseItemId { get; }

        public string ItemNameStringResourceId { get; }

        public ImageResourceDto ItemIconResourceDto { get; }

        public IReadOnlyCollection<string> EnchantmentDefinitionIds { get; }
    }
}
