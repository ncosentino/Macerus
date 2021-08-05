using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Inventory.Api.HoverCards;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards
{
    public sealed class BehaviorsToHoverCardPartViewModelConverterFacade : IBehaviorsToHoverCardPartViewModelConverterFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableBehaviorsToHoverCardPartViewModelConverter>> _lazyConverters;

        public BehaviorsToHoverCardPartViewModelConverterFacade(
            Lazy<IEnumerable<IDiscoverableBehaviorsToHoverCardPartViewModelConverter>> lazyConverters,
            Lazy<IHoverCardPartConverterLoadOrder> lazyHoverCardPartConverterLoadOrder)
        {
            _lazyConverters = new Lazy<IReadOnlyCollection<IDiscoverableBehaviorsToHoverCardPartViewModelConverter>>(() =>
                lazyConverters
                    .Value
                    .OrderBy(lazyHoverCardPartConverterLoadOrder.Value.GetOrder)
                    .ToArray());
        }

        public IEnumerable<IHoverCardPartViewModel> Convert(IEnumerable<IBehavior> behaviors)
        {
            var viewModels = _lazyConverters
                .Value
                .SelectMany(c => c.Convert(behaviors));
            return viewModels;
        }
    }
}
