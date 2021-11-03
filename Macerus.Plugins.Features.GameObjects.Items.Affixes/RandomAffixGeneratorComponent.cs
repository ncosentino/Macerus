using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default
{
    public sealed class RandomAffixGeneratorComponent : IGeneratorComponent
    {
        public RandomAffixGeneratorComponent(
            int minimumAffixes,
            int maximumAffixes,
            IEnumerable<IFilterAttribute> affixDefinitionFilter)
        {
            MinimumAffixes = minimumAffixes;
            MaximumAffixes = maximumAffixes;
            AffixDefinitionFilter = affixDefinitionFilter.ToArray();
        }

        public int MinimumAffixes { get; }

        public int MaximumAffixes { get; }

        public IReadOnlyCollection<IFilterAttribute> AffixDefinitionFilter { get; }
    }
}