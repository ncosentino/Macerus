using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class AffixDto
    {
        public AffixDto(
            string affixId, 
            string affixType,
            int levelMinimum,
            int levelMaximum,
            string prefixStringResourceId, 
            string suffixStringResourceId,
            string mutexKey,
            IReadOnlyCollection<string> enchantmentDefinitionIds)
        {
            AffixId = affixId;
            AffixType = affixType;
            LevelMinimum = levelMinimum;
            LevelMaximum = levelMaximum;
            PrefixStringResourceId = prefixStringResourceId;
            SuffixStringResourceId = suffixStringResourceId;
            MutexKey = mutexKey;
            EnchantmentDefinitionIds = enchantmentDefinitionIds;
        }

        public string AffixId { get; }

        public string AffixType { get; }

        public int LevelMinimum { get; }

        public int LevelMaximum { get; }

        public string PrefixStringResourceId { get; }

        public string SuffixStringResourceId { get; }

        public string MutexKey { get; }
        
        public IReadOnlyCollection<string> EnchantmentDefinitionIds { get; }
    }
}
