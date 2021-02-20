using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class IconFilterComponent : IFilterComponent
    {
        public IconFilterComponent(string iconResource)
        {
            IconResource = iconResource;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = Enumerable.Empty<IFilterAttribute>();

        public string IconResource { get; }
    }
}
