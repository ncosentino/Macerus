﻿
using Macerus.Api.Behaviors.Filtering;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

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

            // FIXME: actually load the map from a repo
            var mapId = new StringIdentifier("swamp");
            _mapManager.SwitchMap(mapId);
        }
    }
}
