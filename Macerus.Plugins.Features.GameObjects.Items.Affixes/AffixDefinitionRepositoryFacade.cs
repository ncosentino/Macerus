using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Api;

using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default
{
    public sealed class AffixDefinitionRepositoryFacade : IReadOnlyAffixDefinitionRepositoryFacade
    {
        private readonly Lazy<IReadOnlyCollection<IReadOnlyAffixDefinitionRepository>> _lazyRepositories;

        public AffixDefinitionRepositoryFacade(Lazy<IEnumerable<IDiscoverableReadOnlyAffixDefinitionRepository>> lazyRepositories)
        {
            _lazyRepositories = new Lazy<IReadOnlyCollection<IReadOnlyAffixDefinitionRepository>>(() =>
                lazyRepositories.Value.ToArray());
        }

        public IEnumerable<IAffixDefinition> GetAffixDefinitions(IFilterContext filterContext) =>
            _lazyRepositories.Value.SelectMany(x => x.GetAffixDefinitions(filterContext)); // FIXME: need to respect the counts...
    }
}