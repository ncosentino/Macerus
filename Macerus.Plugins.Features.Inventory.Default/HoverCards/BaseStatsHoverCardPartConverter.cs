using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Inventory.Api.HoverCards;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards
{
    public sealed class BaseStatsHoverCardPartConverter : IDiscoverableBehaviorsToHoverCardPartViewModelConverter
    {
        private readonly Lazy<IStatResourceProvider> _lazyStatResourceProvider;

        public BaseStatsHoverCardPartConverter(Lazy<IStatResourceProvider> lazyStatResourceProvider)
        {
            _lazyStatResourceProvider = lazyStatResourceProvider;
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
                    _lazyStatResourceProvider.Value.GetStatName(x.Key),
                    x.Value));
            yield return new BaseStatsHoverCardPartViewModel(namesAndValues);
        }
    }
}
