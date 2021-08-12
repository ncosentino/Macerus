using System;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Actors.Triggers;

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
        private static readonly ILevelUpTriggerMechanicSource _levelUpTriggerMechanicSource;

        static LevelUpTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);
            _assertionHelpers = new AssertionHelpers(_container);

            _actorIdentifiers = _container.Resolve<IMacerusActorIdentifiers>();
            _levelUpTriggerMechanicSource = _container.Resolve<ILevelUpTriggerMechanicSource>();

            // NOTE: we need this to trigger systems to register
            _container.Resolve<IGameEngine>();
        }

        [Fact]
        private async Task MutateStats_CurrentExperienceGreaterThanNextExperience_SingleLevelUp()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async player =>
            {
                var hasStatsBehavior = player.GetFirst<IHasStatsBehavior>();

                var baseStatsChangedCount = 0;
                hasStatsBehavior.BaseStatsChanged += (s, e) => baseStatsChangedCount++;

                var triggerMechanicCount = 0;
                var lastLevel = -1;
                _levelUpTriggerMechanicSource.RegisterTrigger(new TestLevelUpTriggerMechanic((actor, level) =>
                {
                    Assert.True(
                        level > lastLevel,
                        $"Level up triggers arrived out of order. Current " +
                        $"trigger is for level {level} but last level was for " +
                        $"level {lastLevel}.");
                    triggerMechanicCount++;
                    lastLevel = level;
                }));

                await hasStatsBehavior.MutateStatsAsync(async stats =>
                {
                    stats[_actorIdentifiers.LevelStatDefinitionId] = 11;
                    stats[_actorIdentifiers.ExperienceForNextLevelStatDefinitionId] = 123;
                    stats[_actorIdentifiers.CurrentExperienceStatDefinitionId] = 150;
                });

                Assert.Equal(1, triggerMechanicCount);
                Assert.Equal(2, baseStatsChangedCount);
                Assert.Equal(12, hasStatsBehavior.BaseStats[_actorIdentifiers.LevelStatDefinitionId]);
                Assert.Equal(13 * 100, hasStatsBehavior.BaseStats[_actorIdentifiers.ExperienceForNextLevelStatDefinitionId]);
                Assert.Equal(150 - 123, hasStatsBehavior.BaseStats[_actorIdentifiers.CurrentExperienceStatDefinitionId]);
                Assert.Equal(5, hasStatsBehavior.BaseStats[_actorIdentifiers.AttributePointsStatDefinitionId]);
                Assert.Equal(1, hasStatsBehavior.BaseStats[_actorIdentifiers.SkillPointsStatDefinitionId]);
                Assert.Equal(1, hasStatsBehavior.BaseStats[_actorIdentifiers.AbilityPointsStatDefinitionId]);
            });
        }

        [Fact]
        private async Task MutateStats_CurrentExperienceMuchGreaterThanNextExperience_MultiLevelUp()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async player =>
            {
                var hasStatsBehavior = player.GetFirst<IHasStatsBehavior>();

                var baseStatsChangedCount = 0;
                hasStatsBehavior.BaseStatsChanged += (s, e) => baseStatsChangedCount++;

                var triggerMechanicCount = 0;
                var lastLevel = -1;
                _levelUpTriggerMechanicSource.RegisterTrigger(new TestLevelUpTriggerMechanic((actor, level) =>
                {
                    Assert.True(
                        level > lastLevel,
                        $"Level up triggers arrived out of order. Current " +
                        $"trigger is for level {level} but last level was for " +
                        $"level {lastLevel}.");
                    triggerMechanicCount++;
                    lastLevel = level;
                }));

                await hasStatsBehavior.MutateStatsAsync(async stats =>
                {
                    stats[_actorIdentifiers.LevelStatDefinitionId] = 1;
                    stats[_actorIdentifiers.ExperienceForNextLevelStatDefinitionId] = 200;
                    stats[_actorIdentifiers.CurrentExperienceStatDefinitionId] = 500;
                });

                Assert.Equal(2, triggerMechanicCount);
                Assert.Equal(3, baseStatsChangedCount);
                Assert.Equal(3, hasStatsBehavior.BaseStats[_actorIdentifiers.LevelStatDefinitionId]);
                Assert.Equal(4 * 100, hasStatsBehavior.BaseStats[_actorIdentifiers.ExperienceForNextLevelStatDefinitionId]);
                Assert.Equal(0, hasStatsBehavior.BaseStats[_actorIdentifiers.CurrentExperienceStatDefinitionId]);
                Assert.Equal(10, hasStatsBehavior.BaseStats[_actorIdentifiers.AttributePointsStatDefinitionId]);
                Assert.Equal(2, hasStatsBehavior.BaseStats[_actorIdentifiers.SkillPointsStatDefinitionId]);
                Assert.Equal(2, hasStatsBehavior.BaseStats[_actorIdentifiers.AbilityPointsStatDefinitionId]);
            });
        }

        private sealed class TestLevelUpTriggerMechanic : ILevelUpTriggerMechanic
        {
            private readonly Action<IGameObject, int> _callback;

            public TestLevelUpTriggerMechanic(Action<IGameObject, int> callback)
            {
                _callback = callback;
            }

            public async Task ActorLevelUpTriggeredAsync(IGameObject actor, int level) => _callback(actor, level);
        }
    }
}
