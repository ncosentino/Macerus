
using Macerus.Api.Behaviors.Filtering;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterMapLoadStartHandler : IDiscoverableStartEncounterHandler
    {
        private readonly IMapManager _mapManager;
        private readonly IFilterContextAmenity _filterContextAmenity;

        public EncounterMapLoadStartHandler(
            IFilterContextAmenity filterContextAmenity,
            IMapManager mapManager)
        {
            _filterContextAmenity = filterContextAmenity;
            _mapManager = mapManager;
        }

        public int Priority => 10000;

        public void Handle(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            if (!encounter.TryGetFirst<IEncounterMapFilterBehavior>(out var encounterMapFilterBehavior))
            {
                return;
            }

            var mapContext = _filterContextAmenity.CopyWithAdditionalAttributes(
                filterContext,
                encounterMapFilterBehavior.ProvidedAttributes);
            _mapManager.SwitchMap(mapContext);
        }
    }
}
