using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class DropTableDto
    {
        public DropTableDto(
            string dropTableId,
            int minimumDrop, 
            int maximumDrop, 
            int requiredItemLevel, 
            int? providedItemLevel, 
            IReadOnlyCollection<string> providedAffixTypes,
            IReadOnlyCollection<KeyValuePair<string, double>> linkedTables)
        {
            DropTableId = dropTableId;
            MinimumDrop = minimumDrop;
            MaximumDrop = maximumDrop;
            RequiredItemLevel = requiredItemLevel;
            ProvidedItemLevel = providedItemLevel;
            ProvidedAffixTypes = providedAffixTypes;
            LinkedTables = linkedTables;
        }

        public string DropTableId { get; }

        public int MinimumDrop { get; }

        public int MaximumDrop { get; }

        public int RequiredItemLevel { get; }

        public int? ProvidedItemLevel { get; }

        public IReadOnlyCollection<string> ProvidedAffixTypes { get; }

        public IReadOnlyCollection<KeyValuePair<string, double>> LinkedTables { get; }
    }
}
