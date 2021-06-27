using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Mapping;
using Macerus.Plugins.Features.Stats.Api;
using Macerus.Plugins.Features.StatusBar.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.TurnBased;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.StatusBar.Default
{
    public sealed class StatusBarController : IStatusBarController
    {
        private readonly IStatusBarViewModel _statusBarViewModel;
        private readonly ISkillUsage _skillUsage;
        private readonly ISkillHandlerFacade _skillHandlerFacade;
        private readonly ITurnBasedManager _turnBasedManager;
        private readonly Lazy<IMapTraversableHighlighter> _mapTraversableHighlighter;
        private readonly ISkillTargetingAmenity _skillTargetingAmenity;
        private readonly ISkillAmenity _skillAmenity;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;

        private readonly Lazy<IReadOnlyCollection<Tuple<IIdentifier, IIdentifier, IIdentifier>>> _lazyCurrentAndMaxResourceIdentifiers;

        public StatusBarController(
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            IReadOnlyMapGameObjectManager readOnlyMapGameObjectManager,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            IStatusBarViewModel statusBarViewModel,
            ISkillUsage skillUsage,
            ISkillHandlerFacade skillHandlerFacade,
            ITurnBasedManager turnBasedManager,
            Lazy<IMapTraversableHighlighter> mapTraversableHighlighter,
            ISkillTargetingAmenity skillTargetingAmenity,
            ISkillAmenity skillAmenity)
        {
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _mapGameObjectManager = readOnlyMapGameObjectManager;
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
            _statusBarViewModel = statusBarViewModel;
            _skillUsage = skillUsage;
            _skillHandlerFacade = skillHandlerFacade;
            _turnBasedManager = turnBasedManager;
            _mapTraversableHighlighter = mapTraversableHighlighter;
            _skillTargetingAmenity = skillTargetingAmenity;
            _skillAmenity = skillAmenity;
            _lazyCurrentAndMaxResourceIdentifiers = new Lazy<IReadOnlyCollection<Tuple<IIdentifier, IIdentifier, IIdentifier>>>(() =>
            {
                // FIXME: make the key a resource name identifier for localized lookup?
                var stats = new Dictionary<string, Tuple<string, string>>()
                {
                    { "LIFE", Tuple.Create("LIFE_CURRENT", "LIFE_MAXIMUM") },
                    { "MANA", Tuple.Create("MANA_CURRENT", "MANA_MAXIMUM") },
                };

                var minMaxIdentifiers = new List<Tuple<IIdentifier, IIdentifier, IIdentifier>>(stats.Count);
                foreach (var stat in stats)
                {
                    var currentId = _statDefinitionToTermMappingRepository
                        .GetStatDefinitionToTermMappingByTerm(stat.Value.Item1)
                        .StatDefinitionId;
                    var maxId = _statDefinitionToTermMappingRepository
                        .GetStatDefinitionToTermMappingByTerm(stat.Value.Item2)
                        .StatDefinitionId;
                    minMaxIdentifiers.Add(Tuple.Create(currentId, maxId, (IIdentifier)new StringIdentifier(stat.Key)));
                }

                return minMaxIdentifiers;
            });

            // FIXME: MySQL implementations at least before .NET 4.5 have
            // issues with async await support and can literally deadlock
            object hackToResolveResourceIds = _lazyCurrentAndMaxResourceIdentifiers.Value;
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
                        
            var resourcesViewModels = await GetResourceViewModelsAsync(
                player,
                _lazyCurrentAndMaxResourceIdentifiers.Value)
                .ConfigureAwait(false);

            _statusBarViewModel.UpdateResource(resourcesViewModels.First(), true);
            _statusBarViewModel.UpdateResource(resourcesViewModels.Last(), false);

            var skills = player
                .GetOnly<IHasSkillsBehavior>()
                .Skills
                .Where(x => !x.Has<IHasEnchantmentsBehavior>())
                .ToArray();
            var abilityViewModels = await CreateAbilityViewModelsAsync(
                player, 
                skills)
                .ConfigureAwait(false);

            _statusBarViewModel.UpdateAbilities(abilityViewModels);
        }

        public async Task ActivateSkillSlotAsync(
            IGameObject actor,
            int slotIndex)
        {
            // FIXME: we actually need to map the index to some sort of quick
            // slot concept, not just full list of skills
            var skills = actor
                .GetOnly<IHasSkillsBehavior>()
                .Skills
                .ToArray();
            var skill = skills[slotIndex];

            if (!await _skillUsage
                .CanUseSkillAsync(
                    actor,
                    skill)
                .ConfigureAwait(false))
            {
                return;
            }

            _skillUsage.UseRequiredResources(
                actor,
                skill);
            _skillHandlerFacade.Handle(
                actor,
                skill);
            _turnBasedManager.SetApplicableObjects(new[] { actor });
        }

        public async Task PreviewSkillSlotAsync(
            IGameObject actor,
            int slotIndex)
        {
            var skillTargetLocations = new Dictionary<int, HashSet<Vector2>>();

            // FIXME: we actually need to map the index to some sort of quick
            // slot concept, not just full list of skills
            var skills = actor
                .GetOnly<IHasSkillsBehavior>()
                .Skills
                .ToArray();
            var skill = skills[slotIndex];

            if (!await _skillUsage
                .CanUseSkillAsync(
                    actor,
                    skill)
                .ConfigureAwait(false))
            {
                _mapTraversableHighlighter
                    .Value
                    .SetTargettedTiles(skillTargetLocations);
                return;
            }

            foreach (var s in _skillAmenity.GetSkillsFromCombination(skill))
            {
                var targetsByTeam = _skillTargetingAmenity.FindTargetLocationsForSkill(
                    actor,
                    s);

                if (!skillTargetLocations.ContainsKey(targetsByTeam.Item1))
                {
                    skillTargetLocations.Add(targetsByTeam.Item1, new HashSet<Vector2>());
                }

                foreach (var t in targetsByTeam.Item2)
                {
                    skillTargetLocations[targetsByTeam.Item1].Add(t);
                }
            }

            _mapTraversableHighlighter
                .Value
                .SetTargettedTiles(skillTargetLocations);
        }

        private async Task<List<IStatusBarResourceViewModel>> GetResourceViewModelsAsync(
            IGameObject actor,
            IEnumerable<Tuple<IIdentifier, IIdentifier, IIdentifier>> currentAndMaxStatIdentifiers)
        {
            var resourcesViewModels = new List<IStatusBarResourceViewModel>();

            var resources = await _statCalculationServiceAmenity
                .GetStatValuesAsync(
                    actor,
                    currentAndMaxStatIdentifiers.SelectMany(x => new[] { x.Item1, x.Item2 }))
                .ConfigureAwait(false);

            foreach (var currentAndMaxIdentifiers in currentAndMaxStatIdentifiers)
            {
                var resourceCurrent = resources[currentAndMaxIdentifiers.Item1];
                var resourceMaximum = resources[currentAndMaxIdentifiers.Item2];

                // FIXME: make this a localized lookup!
                var resourceName = currentAndMaxIdentifiers.Item3.ToString();

                resourcesViewModels.Add(new StatusBarResourceViewModel(
                    resourceName,
                    resourceCurrent,
                    resourceMaximum));
            }

            return resourcesViewModels;
        }

        private async Task<IReadOnlyCollection<IStatusBarAbilityViewModel>> CreateAbilityViewModelsAsync(
            IGameObject actor,
            IReadOnlyCollection<IGameObject> skills)
        {
            var viewModels = new ConcurrentDictionary<IGameObject, IStatusBarAbilityViewModel>();

            // FIXME: convert to IAsyncEnumerable when supported?
            var tasks = skills
                .Select(skill => Task.Run(async () =>
                {
                    var canUseSkill = await _skillUsage.CanUseSkillAsync(actor, skill);
                    var viewModel = new StatusBarAbilityViewModel(
                        canUseSkill,
                        skill.GetOnly<IHasDisplayIconBehavior>().IconResourceId,
                        skill.GetOnly<IHasDisplayNameBehavior>().DisplayName);
                    viewModels.TryAdd(skill, viewModel);
                }))
                .ToArray();
            await Task.WhenAll(tasks).ConfigureAwait(false);

            // we do this to respect the order of the skills
            return skills.Select(x => viewModels[x]).ToArray();
        }
    }
}