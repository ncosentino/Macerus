﻿using System.Threading.Tasks;

using Macerus.Api.Behaviors.Filtering;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Encounters.Default.StartHandlers
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

        public async Task HandleAsync(
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
            await _mapManager
                .SwitchMapAsync(mapContext)
                .ConfigureAwait(false);
        }
    }
}
