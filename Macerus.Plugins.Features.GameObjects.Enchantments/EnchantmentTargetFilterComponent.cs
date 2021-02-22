using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class EnchantmentTargetFilterComponent : IFilterComponent
    {
        public EnchantmentTargetFilterComponent(IIdentifier target)
        {
            Target = target;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; set; } = Enumerable.Empty<IFilterAttribute>();

        public IIdentifier Target { get; set; }
    }
}
