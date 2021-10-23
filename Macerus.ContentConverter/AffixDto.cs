using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class AffixDto
    {
        public AffixDto(
            string affixId, 
            string affixType,
            string prefixStringResourceId, 
            string suffixStringResourceId,
            IReadOnlyCollection<string> enchantmentDefinitionIds)
        {
            AffixId = affixId;
            AffixType = affixType;
            PrefixStringResourceId = prefixStringResourceId;
            SuffixStringResourceId = suffixStringResourceId;
            EnchantmentDefinitionIds = enchantmentDefinitionIds;
        }

        public string AffixId { get; }

        public string AffixType { get; }

        public string PrefixStringResourceId { get; }

        public string SuffixStringResourceId { get; }

        public IReadOnlyCollection<string> EnchantmentDefinitionIds { get; }
    }
}
