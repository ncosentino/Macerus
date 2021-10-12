using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default
{
    public sealed class AffixEnchantmentsGeneratorComponent : IGeneratorComponent
    {
        public AffixEnchantmentsGeneratorComponent(
            int minimumEnchantments,
            int maximumEnchantments,
            IEnumerable<IFilterAttribute> enchantmentDefinitionFilter)
        {
            MinimumEnchantments = minimumEnchantments;
            MaximumEnchantments = maximumEnchantments;
            EnchantmentDefinitionFilter = enchantmentDefinitionFilter.ToArray();
        }

        public int MinimumEnchantments { get; }

        public int MaximumEnchantments { get; }

        public IReadOnlyCollection<IFilterAttribute> EnchantmentDefinitionFilter { get; }
    }
}