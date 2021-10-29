using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class EnchantmentsGeneratorComponent : IGeneratorComponent
    {
        public EnchantmentsGeneratorComponent(
            IEnumerable<IReadOnlyCollection<IFilterAttribute>> filtersForEachEnchantmentDefinition)
        {
            FiltersForEachEnchantmentDefinition = filtersForEachEnchantmentDefinition.ToArray();
        }

        public IReadOnlyCollection<IReadOnlyCollection<IFilterAttribute>> FiltersForEachEnchantmentDefinition { get; }
    }
}