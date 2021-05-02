using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
// TODO: This Stats reference should be to an API!
using Macerus.Plugins.Features.Stats;
using Macerus.Plugins.Features.StatusBar.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace Macerus.Plugins.Features.StatusBar.Default
{
    public sealed class StatusBarController : IStatusBarController
    {
        private readonly IStatusBarViewModel _statusBarViewModel;
        private readonly ISkillUsage _skillUsage;
        private readonly ISkillHandlerFacade _skillHandlerFacade;
        private readonly ITurnBasedManager _turnBasedManager;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;

        public StatusBarController(
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            IReadOnlyMapGameObjectManager readOnlyMapGameObjectManager,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            IStatusBarViewModel statusBarViewModel,
            ISkillUsage skillUsage,
            ISkillHandlerFacade skillHandlerFacade,
            ITurnBasedManager turnBasedManager)
        {
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _mapGameObjectManager = readOnlyMapGameObjectManager;
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
            _statusBarViewModel = statusBarViewModel;
            _skillUsage = skillUsage;
            _skillHandlerFacade = skillHandlerFacade;
            _turnBasedManager = turnBasedManager;
        }

        public delegate StatusBarController Factory();

        public double UpdateIntervalInSeconds { get; } = 0.3;

        public async Task UpdateAsync(ISystemUpdateContext context)
        {
            var player = _mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.Has<IPlayerControlledBehavior>());

            // no player loaded up? no status to update.
            if (player == null)
            {
                return;
            }

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

            var skills = player.GetOnly<IHasSkillsBehavior>().Skills;
            var abilityViewModels = skills
                .Where(x => !x.Has<IHasEnchantmentsBehavior>())
                .Select(x => new StatusBarAbilityViewModel(
                    _skillUsage.CanUseSkill(player, x),
                    x.GetOnly<IHasDisplayIconBehavior>().IconResourceId,
                    x.GetOnly<IHasDisplayNameBehavior>().DisplayName))
                .ToArray();

            _statusBarViewModel.UpdateAbilities(abilityViewModels);
        }

        public void ActivateSkillSlot(int slotIndex)
        {
            var player = _mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.Has<IPlayerControlledBehavior>());
            // FIXME: we actually need to map the index to some sort of quick
            // slot concept, not just full list of skills
            var skills = player
                .GetOnly<IHasSkillsBehavior>()
                .Skills
                .ToArray();
            var skill = skills[slotIndex];

            if (!_skillUsage.CanUseSkill(
                player,
                skill))
            {
                return;
            }

            _skillUsage.UseRequiredResources(
                player,
                skill);
            _skillHandlerFacade.Handle(
                player,
                skill);
            _turnBasedManager.SetApplicableObjects(new[] { player });
        }
    }
}