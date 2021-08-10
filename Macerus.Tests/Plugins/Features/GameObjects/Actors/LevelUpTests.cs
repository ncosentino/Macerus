using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Actors
{
    public sealed class LevelUpTests
    {
        private static readonly MacerusContainer _container;
        private static readonly AssertionHelpers _assertionHelpers;
        private static readonly TestAmenities _testAmenities;

        private static readonly IMacerusActorIdentifiers _actorIdentifiers;

        static LevelUpTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);
            _assertionHelpers = new AssertionHelpers(_container);

            _actorIdentifiers = _container.Resolve<IMacerusActorIdentifiers>();

            // NOTE: we need this to trigger systems to register
            _container.Resolve<IGameEngine>();
        }

        [Fact]
        private async Task MutateStats_CurrentExperienceGreaterThanNextExperience_LevelUpAndExpectedStats()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async player =>
            {
                var hasStatsBehavior = player.GetFirst<IHasStatsBehavior>();

                var baseStatsChangedCount = 0;
                hasStatsBehavior.BaseStatsChanged += (s, e) => baseStatsChangedCount++;

                hasStatsBehavior.MutateStats(stats =>
                {
                    stats[_actorIdentifiers.LevelStatDefinitionId] = 11;
                    stats[_actorIdentifiers.ExperienceForNextLevelStatDefinitionId] = 123;
                    stats[_actorIdentifiers.CurrentExperienceStatDefinitionId] = 150;
                });

                Assert.Equal(2, baseStatsChangedCount);
                Assert.Equal(12, hasStatsBehavior.BaseStats[_actorIdentifiers.LevelStatDefinitionId]);
                Assert.Equal(12 * 100, hasStatsBehavior.BaseStats[_actorIdentifiers.ExperienceForNextLevelStatDefinitionId]);
                Assert.Equal(150 - 123, hasStatsBehavior.BaseStats[_actorIdentifiers.CurrentExperienceStatDefinitionId]);
                Assert.Equal(5, hasStatsBehavior.BaseStats[_actorIdentifiers.AttributePointsStatDefinitionId]);
                Assert.Equal(1, hasStatsBehavior.BaseStats[_actorIdentifiers.SkillPointsStatDefinitionId]);
                Assert.Equal(1, hasStatsBehavior.BaseStats[_actorIdentifiers.AbilityPointsStatDefinitionId]);
            });
        }
    }
}
