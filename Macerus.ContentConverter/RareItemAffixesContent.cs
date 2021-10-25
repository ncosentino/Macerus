namespace Macerus.ContentConverter
{
    public sealed class RareItemAffixesContent
    {
        public RareItemAffixesContent(
            RareItemAffixDto rareItemAffixDto, 
            StringResourceDto stringResourceDto)
        {
            RareItemAffixDto = rareItemAffixDto;
            StringResourceDto = stringResourceDto;
        }

        public RareItemAffixDto RareItemAffixDto { get; }

        public StringResourceDto StringResourceDto { get; }
    }
}
