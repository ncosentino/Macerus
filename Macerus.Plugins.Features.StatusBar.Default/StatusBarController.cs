﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Mapping;
using Macerus.Plugins.Features.Stats.Api;
using Macerus.Plugins.Features.StatusBar.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.PartyManagement;
using ProjectXyz.Plugins.Features.TurnBased;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.StatusBar.Default
{
    public sealed class StatusBarController : IStatusBarController
    {
        private readonly IStatusBarViewModel _statusBarViewModel;
        private readonly ISkillUsage _skillUsage;
        private readonly ISkillHandlerFacade _skillHandlerFacade;
        private readonly Lazy<ITurnBasedManager> _lazyTurnBasedManager;
        private readonly Lazy<IMapTraversableHighlighter> _mapTraversableHighlighter;
        private readonly ISkillTargetingAmenity _skillTargetingAmenity;
        private readonly ISkillAmenity _skillAmenity;
        private readonly Lazy<ICombatTurnManager> _lazyCombatTurnManager;
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly Lazy<IStatCalculationServiceAmenity> _lazyStatCalculationServiceAmenity;
        private readonly Lazy<IReadOnlyRosterManager> _lazyRosterManager;
        private readonly IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;

        private readonly Lazy<IReadOnlyCollection<Tuple<IIdentifier, IIdentifier, IIdentifier>>> _lazyCurrentAndMaxResourceIdentifiers;

        public StatusBarController(
            Lazy<IStatCalculationServiceAmenity> lazyStatCalculationServiceAmenity,
            Lazy<IReadOnlyRosterManager> lazyRosterManager,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            IStatusBarViewModel statusBarViewModel,
            ISkillUsage skillUsage,
            ISkillHandlerFacade skillHandlerFacade,
            Lazy<ITurnBasedManager> lazyTurnBasedManager,
            Lazy<IMapTraversableHighlighter> mapTraversableHighlighter,
            ISkillTargetingAmenity skillTargetingAmenity,
            ISkillAmenity skillAmenity,
            Lazy<ICombatTurnManager> lazyCombatTurnManager,
            IFilterContextProvider filterContextProvider)
        {
            _lazyStatCalculationServiceAmenity = lazyStatCalculationServiceAmenity;
            _lazyRosterManager = lazyRosterManager;
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
            _statusBarViewModel = statusBarViewModel;
            _skillUsage = skillUsage;
            _skillHandlerFacade = skillHandlerFacade;
            _lazyTurnBasedManager = lazyTurnBasedManager;
            _mapTraversableHighlighter = mapTraversableHighlighter;
            _skillTargetingAmenity = skillTargetingAmenity;
            _skillAmenity = skillAmenity;
            _lazyCombatTurnManager = lazyCombatTurnManager;
            _filterContextProvider = filterContextProvider;
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

            _statusBarViewModel.RequestCompleteTurn += StatusBarViewModel_RequestCompleteTurn;
        }

        public delegate StatusBarController Factory();

        public double UpdateIntervalInSeconds { get; } = 0.3;

        public async Task UpdateAsync(ISystemUpdateContext context)
        {
            if (!_statusBarViewModel.IsOpen)
            {
                return;
            }

            var player = _lazyRosterManager.Value.ActiveControlledActor;
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

            _statusBarViewModel.CanCompleteTurn = await GetCanCompleteTurn(player)
                .ConfigureAwait(false);
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
            if (slotIndex < 0 || slotIndex >= skills.Length)
            {
                return;
            }

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
            await ClearSkillSlotPreviewAsync().ConfigureAwait(false);
        }

        public async Task ClearSkillSlotPreviewAsync()
        {
            var skillTargetLocations = new Dictionary<int, HashSet<Vector2>>();
            _mapTraversableHighlighter
                .Value
                .SetTargettedTiles(skillTargetLocations);
        }

        public async Task PreviewSkillSlotAsync(
            IGameObject actor,
            int slotIndex)
        {
            // FIXME: we actually need to map the index to some sort of quick
            // slot concept, not just full list of skills
            var skills = actor
                .GetOnly<IHasSkillsBehavior>()
                .Skills
                .ToArray();
            if (slotIndex < 0 || slotIndex >= skills.Length)
            {
                return;
            }

            var skill = skills[slotIndex];
            var skillTargetLocations = new Dictionary<int, HashSet<Vector2>>();

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

        private async Task<bool> GetCanCompleteTurn(IGameObject actor)
        {
            if (!_lazyCombatTurnManager.Value.InCombat)
            {
                return false;
            }

            var filterContext = _filterContextProvider.GetContext();
            var actorWithCurrentTurn = _lazyCombatTurnManager.Value.GetSnapshot(filterContext, 1).Single();
            var canCompleteTurn = Equals(actor, actorWithCurrentTurn);
            return canCompleteTurn;
        }

        private async Task<List<IStatusBarResourceViewModel>> GetResourceViewModelsAsync(
            IGameObject actor,
            IEnumerable<Tuple<IIdentifier, IIdentifier, IIdentifier>> currentAndMaxStatIdentifiers)
        {
            var resourcesViewModels = new List<IStatusBarResourceViewModel>();

            var resources = await _lazyStatCalculationServiceAmenity
                .Value
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

        private void StatusBarViewModel_RequestCompleteTurn(
            object sender,
            EventArgs e)
        {
            var actor = _lazyRosterManager.Value.ActiveControlledActor;
            Contract.RequiresNotNull(
                actor,
                $"Could not get the active player-controlled actor.");
            _lazyTurnBasedManager
                .Value
                .SetApplicableObjects(new[] { actor });
        }
    }
}