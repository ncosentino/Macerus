using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Inventory.Api.HoverCards;
using Macerus.Plugins.Features.Resources;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards
{
    public sealed class BaseStatsHoverCardPartConverter : IDiscoverableBehaviorsToHoverCardPartViewModelConverter
    {
        private readonly Lazy<IStringResourceProvider> _lazyStringResourceProvider;

        public BaseStatsHoverCardPartConverter(Lazy<IStringResourceProvider> lazyStringResourceProvider)
        {
            _lazyStringResourceProvider = lazyStringResourceProvider;
        }

        public IEnumerable<IHoverCardPartViewModel> Convert(IEnumerable<IBehavior> behaviors)
        {
            if (!behaviors.TryGetFirst<IHasStatsBehavior>(out var hasStatsBehavior))
            {
                yield break;
            }

            var namesAndValues = hasStatsBehavior
                .BaseStats
                .Select(x => Tuple.Create(
                    _lazyStringResourceProvider.Value.GetString(x.Key),
                    x.Value));
            yield return new BaseStatsHoverCardPartViewModel(namesAndValues);
        }
    }
}
