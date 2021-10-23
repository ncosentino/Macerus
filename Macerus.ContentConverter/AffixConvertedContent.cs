using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class AffixConvertedContent
    {
        public AffixConvertedContent(
            IReadOnlyCollection<AffixDto> affixDtos, 
            IReadOnlyCollection<StringResourceDto> stringResourceDtos,
            IReadOnlyCollection<EnchantmentDefinitionDto> enchantmentDefinitionDtos)
        {
            AffixDtos = affixDtos;
            StringResourceDtos = stringResourceDtos;
            EnchantmentDefinitionDtos = enchantmentDefinitionDtos;
        }

        public IReadOnlyCollection<AffixDto> AffixDtos { get; }

        public IReadOnlyCollection<StringResourceDto> StringResourceDtos { get; }

        public IReadOnlyCollection<EnchantmentDefinitionDto> EnchantmentDefinitionDtos { get; }
    }
}
