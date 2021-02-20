using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class HasStatFilterComponent : IFilterComponent
    {
        public HasStatFilterComponent(IIdentifier statDefinitionId)
        {
            StatDefinitionId = statDefinitionId;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; set; } = Enumerable.Empty<IFilterAttribute>();

        public IIdentifier StatDefinitionId { get; set; }
    }
}
