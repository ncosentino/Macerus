using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class ConvertedResourceContent
    {
        public ConvertedResourceContent(IReadOnlyCollection<string> resourceContentFilePaths)
        {
            ResourceContentFilePaths = resourceContentFilePaths;
        }

        public IReadOnlyCollection<string> ResourceContentFilePaths { get; }
    }
}
