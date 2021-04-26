using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Macerus.Api.Behaviors;
// TODO: This Stats reference should be to an API!
using Macerus.Plugins.Features.Stats;
using Macerus.Plugins.Features.StatusBar.Api;
using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.StatusBar.Default
{
    public sealed class StatusBarController : IStatusBarController
    {
        private readonly IStatusBarViewModel _statusBarViewModel;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;

        public StatusBarController(
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            IReadOnlyMapGameObjectManager readOnlyMapGameObjectManager,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            IStatusBarViewModel statusBarViewModel)
        {
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _mapGameObjectManager = readOnlyMapGameObjectManager;
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
            _statusBarViewModel = statusBarViewModel;
        }

        public delegate StatusBarController Factory();

        public async Task UpdateAsync()
        {
            var player = _mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.Has<IPlayerControlledBehavior>());
            Contract.RequiresNotNull(
                player,
                $"Expecting to find game object on map with behavior '{typeof(IPlayerControlledBehavior)}'.");

            var stats = new Dictionary<string, Tuple<string, string>>()
            {
                {"LIFE", Tuple.Create("LIFE_CURRENT", "LIFE_MAXIMUM") },
                {"MANA", Tuple.Create("MANA_CURRENT", "MANA_MAXIMUM") },
            };

            var resourcesViewModels = new List<IStatusBarResourceViewModel>();

            foreach (var stat in stats)
            {
                var currentId = _statDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm(stat.Value.Item1)
                    .StatDefinitionId;
                var maxId = _statDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm(stat.Value.Item2)
                    .StatDefinitionId;

                var resources = await _statCalculationServiceAmenity.GetStatValuesAsync(
                    player,
                    new[] { currentId, maxId });

                var resourceCurrent = resources[currentId];
                var resourceMaximum = resources[maxId];

                resourcesViewModels.Add(new StatusBarResourceViewModel(
                    stat.Key,
                    resourceCurrent,
                    resourceMaximum));
            }

            _statusBarViewModel.UpdateResource(resourcesViewModels.First(), true);
            _statusBarViewModel.UpdateResource(resourcesViewModels.Last(), false);
        }
    }
}