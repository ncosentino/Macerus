using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class ItemTagsGeneratorComponent : IGeneratorComponent
    {
        public ItemTagsGeneratorComponent(IEnumerable<IIdentifier> tags)
        {
            Tags = tags.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> Tags { get; }
    }
}
