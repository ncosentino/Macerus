using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class HasPrefixFilterComponent : IFilterComponent
    {
        public HasPrefixFilterComponent(IIdentifier prefixId)
        {
            PrefixId = prefixId;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = Enumerable.Empty<IFilterAttribute>();

        public IIdentifier PrefixId { get; }
    }
}
