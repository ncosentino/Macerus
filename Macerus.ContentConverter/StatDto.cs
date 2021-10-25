namespace Macerus.ContentConverter
{
    public sealed class StatDto
    {
        public StatDto(string statDefinitionId, string statTerm)
        {
            StatDefinitionId = statDefinitionId;
            StatTerm = statTerm;
        }

        public string StatDefinitionId { get; }

        public string StatTerm { get; }
    }
}
