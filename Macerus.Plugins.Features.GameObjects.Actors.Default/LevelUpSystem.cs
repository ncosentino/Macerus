using System;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IMacerusActorIdentifiers _actorIdentifiers;

        public LevelUpSystem(
            Lazy<IMapGameObjectManager> lazyMapGameObjectManager,
            IMacerusActorIdentifiers actorIdentifiers)
        {
            _lazyMapGameObjectManager = lazyMapGameObjectManager;
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

        private void HasStatsBehavior_BaseStatsChanged(
            object sender,
            StatsChangedEventArgs e)
        {
            if (!e.ChangedStats.Any(x => Equals(x.Key, _actorIdentifiers.CurrentExperienceStatDefinitionId)) &&
                !e.AddedStats.Any(x => Equals(x.Key, _actorIdentifiers.CurrentExperienceStatDefinitionId)))
            {
                return;
            }

            var hasStatsBehavior = (IHasStatsBehavior)sender;
            var currentXp = hasStatsBehavior.BaseStats[_actorIdentifiers.CurrentExperienceStatDefinitionId];
            var nextXp = hasStatsBehavior.BaseStats[_actorIdentifiers.ExperienceForNextLevelStatDefinitionId];
            if (currentXp < nextXp)
            {
                return;
            }

            hasStatsBehavior.MutateStats(stats =>
            {
                stats[_actorIdentifiers.AttributePointsStatDefinitionId] += 5;
                stats[_actorIdentifiers.SkillPointsStatDefinitionId]++;
                stats[_actorIdentifiers.AbilityPointsStatDefinitionId]++;

                // FIXME: we need a formula here plzkthx
                stats[_actorIdentifiers.ExperienceForNextLevelStatDefinitionId] = (stats[_actorIdentifiers.LevelStatDefinitionId] + 1) * 100;
                stats[_actorIdentifiers.CurrentExperienceStatDefinitionId] = currentXp - nextXp;

                stats[_actorIdentifiers.LevelStatDefinitionId]++;
            });
        }
    }
}
