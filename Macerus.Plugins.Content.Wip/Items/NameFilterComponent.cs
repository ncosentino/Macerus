using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class NameFilterComponent : IFilterComponent
    {
        public NameFilterComponent(string displayName)
        {
            DisplayName = displayName;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = Enumerable.Empty<IFilterAttribute>();

        public string DisplayName { get; }
    }
}
