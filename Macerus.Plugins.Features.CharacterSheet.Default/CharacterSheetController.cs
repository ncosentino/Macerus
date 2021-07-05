using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.CharacterSheet.Api;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace Macerus.Plugins.Features.CharacterSheet.Default
{
    public sealed class CharacterSheetController : ICharacterSheetController
    {
        private readonly ICharacterSheetViewModel _characterSheetViewModel;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly Lazy<IReadOnlyRosterManager> _lazyRosterManager;
        private readonly IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;

        public CharacterSheetController(
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            Lazy<IReadOnlyRosterManager> lazyRosterManager,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            ICharacterSheetViewModel characterSheetViewModel)
        {
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _lazyRosterManager = lazyRosterManager;
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
            var player = _lazyRosterManager.Value.ActiveControlledActor;
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