using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class HasSuffixGeneratorComponent : IGeneratorComponent
    {
        public HasSuffixGeneratorComponent(IIdentifier suffixId)
        {
            SuffixId = suffixId;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; set; } = Enumerable.Empty<IGeneratorAttribute>();

        public IIdentifier SuffixId { get; set; }
    }
}
