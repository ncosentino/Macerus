using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Inventory.Api.HoverCards;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards
{
    public sealed class BehaviorsToHoverCardPartViewModelConverterFacade : IBehaviorsToHoverCardPartViewModelConverterFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableBehaviorsToHoverCardPartViewModelConverter> _converters;

        public BehaviorsToHoverCardPartViewModelConverterFacade(IEnumerable<IDiscoverableBehaviorsToHoverCardPartViewModelConverter> converters)
        {
            _converters = converters.ToArray();
        }

        public IEnumerable<IHoverCardPartViewModel> Convert(IEnumerable<IBehavior> behaviors)
        {
            var viewModels = _converters.SelectMany(c => c.Convert(behaviors));
            return viewModels;
        }
    }
}
