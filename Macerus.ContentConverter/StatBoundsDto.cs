namespace Macerus.ContentConverter
{
    public sealed class StatBoundsDto
    {
        public StatBoundsDto(
            string statDefinitionId,
            string minimumExpression,
            string maximumExpression)
        {
            StatDefinitionId = statDefinitionId;
            MinimumExpression = minimumExpression;
            MaximumExpression = maximumExpression;
        }

        public string StatDefinitionId { get; }

        public string MinimumExpression { get; }

        public string MaximumExpression { get; }
    }
}
