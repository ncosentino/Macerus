using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using Macerus.Plugins.Features.Resources;

using NexusLabs.Framework;

using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Filtering;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public sealed class RareItemNameGenerator : IRareItemNameGenerator
    {
        private readonly IRandom _random;
        private readonly IRareAffixRepositoryFacade _rareAffixRepository;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IStringResourceProvider _stringResourceProvider;

        public RareItemNameGenerator(
            IRandom random,
            IRareAffixRepositoryFacade rareAffixRepository,
            IFilterContextAmenity filterContextAmenity,
            IStringResourceProvider stringResourceProvider)
        {
            _random = random;
            _rareAffixRepository = rareAffixRepository;
            _filterContextAmenity = filterContextAmenity;
            _stringResourceProvider = stringResourceProvider;
        }

        public IHasInventoryDisplayName GenerateName(
            IEnumerable<IBehavior> itemBehaviors,
            IReadOnlyCollection<IGameObject> enchantments,
            IFilterContext filterContext)
        {
            // FIXME: incorporate the passed in filter context properly

            // we need to be able to filter the affixes by the types of items
            // i.e. "Entropy Chain" is a good amulet name, not a good sword name
            var tagsFilter = new AnyTagsFilter(itemBehaviors.Get<ITagsBehavior>().SelectMany(x => x.Tags));          

            var rareAffixFilterContext = _filterContextAmenity.CreateFilterContextForAnyAmount(CreateRareAffixFilter(tagsFilter, true));
            var prefix = _rareAffixRepository
                .GetAffixes(rareAffixFilterContext)
                .Random(_random);

            IRareItemAffix suffix = null;
            while (suffix == null || Equals(prefix.StringResourceId, suffix.StringResourceId))
            {
                rareAffixFilterContext = _filterContextAmenity.CreateFilterContextForAnyAmount(CreateRareAffixFilter(tagsFilter, false));
                suffix = _rareAffixRepository
                    .GetAffixes(rareAffixFilterContext)
                    .Random(_random);
            }

            var prefixName = _stringResourceProvider.GetString(prefix.StringResourceId);
            var suffixName = _stringResourceProvider.GetString(suffix.StringResourceId);
            return new HasInventoryDisplayName($"{prefixName} {suffixName}");
        }

        private IReadOnlyCollection<IFilterAttribute> CreateRareAffixFilter(
            AnyTagsFilter tagsFilter,
            bool prefix)
        {
            var requiredFilters = new List<IFilterAttribute>();
            requiredFilters.Add(_filterContextAmenity.CreateRequiredAttribute(
                new StringIdentifier("is-prefix"),
                prefix));

            if (tagsFilter.Tags.Any())
            {
                requiredFilters.Add(_filterContextAmenity.CreateSupportedAttribute(
                    new StringIdentifier("tags"),
                    tagsFilter));
            }

            return requiredFilters;
        }
    }
}
