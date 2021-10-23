using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class UniqueItemConvertedContent
    {
        public UniqueItemConvertedContent(
            UniqueItemDto uniqueItemDto, 
            IReadOnlyCollection<EnchantmentDefinitionDto> enchantmentDefinitionDtos,
            StringResourceDto stringResourceDto,
            ImageResourceDto imageResourceDto)
        {
            UniqueItemDto = uniqueItemDto;
            EnchantmentDefinitionDtos = enchantmentDefinitionDtos;
            StringResourceDto = stringResourceDto;
            ImageResourceDto = imageResourceDto;
        }

        public UniqueItemDto UniqueItemDto { get; }

        public IReadOnlyCollection<EnchantmentDefinitionDto> EnchantmentDefinitionDtos { get; }

        public StringResourceDto StringResourceDto { get; }

        public ImageResourceDto ImageResourceDto { get; }
    }
}
