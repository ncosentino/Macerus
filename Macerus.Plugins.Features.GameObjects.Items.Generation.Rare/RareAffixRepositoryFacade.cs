using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public sealed class RareAffixRepositoryFacade : IRareAffixRepositoryFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableRareAffixRepository>> _lazyRareAffixRepositories;

        public RareAffixRepositoryFacade(Lazy<IEnumerable<IDiscoverableRareAffixRepository>> lazyRareAffixRepositories)
        {
            _lazyRareAffixRepositories = new Lazy<IReadOnlyCollection<IDiscoverableRareAffixRepository>>(() =>
                lazyRareAffixRepositories.Value.ToArray());
        }

        public IEnumerable<IRareItemAffix> GetAffixes(IFilterContext filterContext)
        {
            var matches = _lazyRareAffixRepositories
                .Value
                .SelectMany(x => x.GetAffixes(filterContext));
            return matches;
        }
    }
}