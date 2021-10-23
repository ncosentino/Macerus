namespace Macerus.ContentConverter
{
    public sealed class EnchantmentDefinitionDto
    {
        public EnchantmentDefinitionDto(
            string enchantmentDefinitionId, 
            string statDefinitionId, 
            string statTerm, 
            string modifier, 
            double rangeMinimum,
            double rangeMaximum, 
            int decimalPlaces)
        {
            EnchantmentDefinitionId = enchantmentDefinitionId;
            StatDefinitionId = statDefinitionId;
            StatTerm = statTerm;
            Modifier = modifier;
            RangeMinimum = rangeMinimum;
            RangeMaximum = rangeMaximum;
            DecimalPlaces = decimalPlaces;
        }

        public string EnchantmentDefinitionId { get; }

        public string StatDefinitionId { get; }

        public string StatTerm { get; }

        public string Modifier { get; }

        public double RangeMinimum { get; }

        public double RangeMaximum { get; }

        public int DecimalPlaces { get; }
    }
}
