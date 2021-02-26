using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Generation;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Generation.MySql
{
    public sealed class EnchantmentDefinition : IEnchantmentDefinition
    {
        public EnchantmentDefinition(
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterComponent> filterComponents)
        {
            SupportedAttributes = supportedAttributes.ToArray();
            FilterComponents = filterComponents.ToArray();
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IFilterComponent> FilterComponents { get; }
    }
}
