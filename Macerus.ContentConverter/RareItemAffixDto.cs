using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class RareItemAffixDto
    {
        public RareItemAffixDto(
            string stringResourceId, 
            string stringResource,
            bool isPrefix, 
            IReadOnlyCollection<string> requiredItemTypes)
        {
            StringResourceId = stringResourceId;
            StringResource = stringResource;
            IsPrefix = isPrefix;
            RequiredItemTypes = requiredItemTypes;
        }

        public string StringResourceId { get; }

        public string StringResource { get; }

        public bool IsPrefix { get; }

        public IReadOnlyCollection<string> RequiredItemTypes { get; }
    }
}
