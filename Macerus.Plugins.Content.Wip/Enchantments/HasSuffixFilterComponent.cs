using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class HasSuffixFilterComponent : IFilterComponent
    {
        public HasSuffixFilterComponent(IIdentifier suffixId)
        {
            SuffixId = suffixId;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; set; } = Enumerable.Empty<IFilterAttribute>();

        public IIdentifier SuffixId { get; set; }
    }
}
