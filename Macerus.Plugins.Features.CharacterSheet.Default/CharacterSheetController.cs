using System;
using System.Collections.Generic;
using System.Linq;
using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.CharacterSheet.Api;
// TODO: This Stats reference should be to an API!
using Macerus.Plugins.Features.Stats;
using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.CharacterSheet.Default
{
    public sealed class CharacterSheetController : ICharacterSheetController
    {
        private readonly ICharacterSheetViewModel _characterSheetViewModel;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;

        public CharacterSheetController(
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            IReadOnlyMapGameObjectManager readOnlyMapGameObjectManager,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            ICharacterSheetViewModel characterSheetViewModel)
        {
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _mapGameObjectManager = readOnlyMapGameObjectManager;
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
            _characterSheetViewModel = characterSheetViewModel;

            _characterSheetViewModel.Opened += CharacterSheetViewModel_Open;
            _characterSheetViewModel.Closed += CharacterSheetViewModel_Closed;
        }

        public delegate CharacterSheetController Factory();

        public void OpenCharacterSheet() => _characterSheetViewModel.Open();

        public void CloseCharacterSheet() => _characterSheetViewModel.Close();

        public bool ToggleCharacterSheet()
        {
            if (!_characterSheetViewModel.IsOpen)
            {
                OpenCharacterSheet();
            }
            else
            {
                CloseCharacterSheet();
            }

            return _characterSheetViewModel.IsOpen;
        }

        private async void CharacterSheetViewModel_Open(
            object sender,
            EventArgs e)
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

            var statsToShow = new List<ICharacterStatViewModel>();

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

                statsToShow.Add(new CharacterStatViewModel(
                    stat.Key,
                    $"{(resourceCurrent / resourceMaximum) * 100}%"));
            }

            _characterSheetViewModel.UpdateStats(statsToShow);
        }

        private void CharacterSheetViewModel_Closed(
            object sender,
            EventArgs e)
        {
            _characterSheetViewModel.UpdateStats(Enumerable.Empty<ICharacterStatViewModel>());
        }
    }
}