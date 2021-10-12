using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Api;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default
{
    public sealed class InMemoryAffixDefinitionRepository : IDiscoverableReadOnlyAffixDefinitionRepository
    {
        private readonly Lazy<IAttributeFilterer> _lazyAttributeFilterer;
        private readonly Lazy<IReadOnlyCollection<IAffixDefinition>> _lazyAffixDefinitions;

        public InMemoryAffixDefinitionRepository(
            Lazy<IAttributeFilterer> lazyAttributeFilterer,
            IEnumerable<IAffixDefinition> affixDefinitions)
        {
            _lazyAttributeFilterer = lazyAttributeFilterer;
            _lazyAffixDefinitions = new Lazy<IReadOnlyCollection<IAffixDefinition>>(() =>
                affixDefinitions.ToArray());
        }

        public IEnumerable<IAffixDefinition> GetAffixDefinitions(IFilterContext filterContext)
        {
            var results = _lazyAttributeFilterer
                .Value
                .BidirectionalFilter(
                    _lazyAffixDefinitions.Value,
                    filterContext.Attributes);
            return results;
        }
    }
}