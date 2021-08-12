using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors.Triggers;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default
{
    public sealed class LevelUpSystem : IDiscoverableSystem
    {
        private readonly Lazy<IMapGameObjectManager> _lazyMapGameObjectManager;
        private readonly Lazy<ILevelUpTriggerMechanicSource> _lazyLevelUpTriggerMechanicSource;
        private readonly IMacerusActorIdentifiers _actorIdentifiers;

        public LevelUpSystem(
            Lazy<IMapGameObjectManager> lazyMapGameObjectManager,
            Lazy<ILevelUpTriggerMechanicSource> lazyLevelUpTriggerMechanicSource,
            IMacerusActorIdentifiers actorIdentifiers)
        {
            _lazyMapGameObjectManager = lazyMapGameObjectManager;
            _lazyLevelUpTriggerMechanicSource = lazyLevelUpTriggerMechanicSource;
            _actorIdentifiers = actorIdentifiers;

            // FIXME: no point in lazy because of this?
            _lazyMapGameObjectManager.Value.Synchronized += MapGameObjectManager_Synchronized;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
        }

        private void MapGameObjectManager_Synchronized(
            object sender,
            GameObjectsSynchronizedEventArgs e)
        {
            foreach (var hasStatsBehavior in e
                .Added
                .Select(x => x.TryGetFirst<IHasStatsBehavior>(out var hasStats) 
                    ? hasStats 
                    : null)
                .Where(x => x != null))
            {
                hasStatsBehavior.BaseStatsChanged += HasStatsBehavior_BaseStatsChanged;
            }

            foreach (var hasStatsBehavior in e
                .Removed
                .Select(x => x.TryGetFirst<IHasStatsBehavior>(out var hasStats)
                    ? hasStats
                    : null)
                .Where(x => x != null))
            {
                hasStatsBehavior.BaseStatsChanged -= HasStatsBehavior_BaseStatsChanged;
            }
        }

        private async void HasStatsBehavior_BaseStatsChanged(
            object sender,
            StatsChangedEventArgs e)
        {
            if (e.ChangedStats.Any(x => Equals(x.Key, _actorIdentifiers.CurrentExperienceStatDefinitionId)) ||
                e.AddedStats.Any(x => Equals(x.Key, _actorIdentifiers.CurrentExperienceStatDefinitionId)))
            {
                await HandleExperienceIncreaseAsync((IHasStatsBehavior)sender)
                    .ConfigureAwait(false);
            }

            var levelChangeEntry = e.ChangedStats.FirstOrDefault(x => 
                Equals(x.Key, _actorIdentifiers.LevelStatDefinitionId) &&
                (x.Value.Item1 - x.Value.Item2) == 1);
            if (levelChangeEntry.Key != default)
            {
                await HandleLevelIncrease(
                    (IHasStatsBehavior)sender,
                    (int)levelChangeEntry.Value.Item1)
                    .ConfigureAwait(false);
            }
        }

        private async Task HandleLevelIncrease(
            IHasStatsBehavior hasStatsBehavior,
            int level)
        {
            await _lazyLevelUpTriggerMechanicSource
                .Value
                .ActorLevelUpTriggeredAsync(
                    hasStatsBehavior.Owner,
                    level)
                .ConfigureAwait(false);
        }

        private async Task HandleExperienceIncreaseAsync(IHasStatsBehavior hasStatsBehavior)
        {
            var currentXp = hasStatsBehavior.BaseStats[_actorIdentifiers.CurrentExperienceStatDefinitionId];
            var nextXp = hasStatsBehavior.BaseStats[_actorIdentifiers.ExperienceForNextLevelStatDefinitionId];
            if (currentXp < nextXp)
            {
                return;
            }

            await hasStatsBehavior.MutateStatsAsync(async stats =>
            {
                stats[_actorIdentifiers.AttributePointsStatDefinitionId] += 5;
                stats[_actorIdentifiers.SkillPointsStatDefinitionId]++;
                stats[_actorIdentifiers.AbilityPointsStatDefinitionId]++;

                stats[_actorIdentifiers.LevelStatDefinitionId]++;
                stats[_actorIdentifiers.CurrentExperienceStatDefinitionId] = currentXp - nextXp;
                // FIXME: we need a formula here plzkthx
                stats[_actorIdentifiers.ExperienceForNextLevelStatDefinitionId] = (stats[_actorIdentifiers.LevelStatDefinitionId] + 1) * 100;
            });
        }
    }
}
