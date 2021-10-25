using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public sealed class InMemoryRareAffixRepository : IDiscoverableRareAffixRepository
    {
        private readonly Lazy<IReadOnlyCollection<IRareItemAffix>> _lazyAffixes;
        private readonly Lazy<IAttributeFilterer> _lazyAttributeFilterer;

        public InMemoryRareAffixRepository(
            Lazy<IAttributeFilterer> lazyAttributeFilterer,
            Lazy<IEnumerable<IRareItemAffix>> lazyAffixes)
        {
            _lazyAffixes = new Lazy<IReadOnlyCollection<IRareItemAffix>>(() =>
                lazyAffixes.Value.ToArray());
            _lazyAttributeFilterer = lazyAttributeFilterer;
        }

        public IEnumerable<IRareItemAffix> GetAffixes(IFilterContext filterContext)
        {
            var matches = _lazyAttributeFilterer
                .Value
                .BidirectionalFilter(
                    _lazyAffixes.Value, 
                    filterContext.Attributes);
            return matches;
        }
    }
}