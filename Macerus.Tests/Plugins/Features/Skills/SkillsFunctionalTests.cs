using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Skills;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Weather
{
    [CollectionDefinition(nameof(SkillsFunctionalTests), DisableParallelization = true)]
    public sealed class SkillsFunctionalTests
    {
        private static readonly MacerusContainer _container;
        private static readonly TestAmenities _testAmenities;
        private static readonly IMapGameObjectManager _mapGameObjectManager;
        private static readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private static readonly ISkillAmenity _skillAmenity;
        private static readonly IGameEngine _gameEngine;
        private static readonly IRealTimeManager _realTimeManager;
        private static readonly ISkillHandlerFacade _skillHandlerFacade;

        static SkillsFunctionalTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);

            _mapGameObjectManager = _container.Resolve<IMapGameObjectManager>();
            _statCalculationServiceAmenity = _container.Resolve<IStatCalculationServiceAmenity>();
            _skillAmenity = _container.Resolve<ISkillAmenity>();
            _gameEngine = _container.Resolve<IGameEngine>();
            _realTimeManager = _container.Resolve<IRealTimeManager>();
            _skillHandlerFacade = _container.Resolve<ISkillHandlerFacade>();
        }

        [Fact]
        private async Task UseSkill_PassiveGreenGlow_ExpectedStatValue()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var player = _testAmenities.CreatePlayerInstance();
                _skillAmenity.EnsureHasSkill(
                    player,
                    new StringIdentifier("passive-green-glow"));

                _mapGameObjectManager.MarkForAddition(player);
                await _mapGameObjectManager
                    .SynchronizeAsync()
                    .ConfigureAwait(false);

                var statValue = await _statCalculationServiceAmenity
                    .GetStatValueAsync(
                        player,
                        new IntIdentifier(8)) // green light radius
                    .ConfigureAwait(false);

                Assert.Equal(1, statValue);
            });            
        }

        [Fact]
        private async Task UseSkill_HealSelf_ExpectedCastAnimation()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var player = _testAmenities.CreatePlayerInstance();
                var statsBehavior = player.GetOnly<IHasStatsBehavior>();

                statsBehavior.MutateStats(stats =>
                {
                    stats[new IntIdentifier(2)] = 1; // life current
                });

                var skill = _skillAmenity.EnsureHasSkill(
                    player,
                    new StringIdentifier("heal"));

                _mapGameObjectManager.MarkForAddition(player);
                await _mapGameObjectManager
                    .SynchronizeAsync()
                    .ConfigureAwait(false);

                await _skillHandlerFacade
                    .HandleSkillAsync(
                        player,
                        skill)
                    .ConfigureAwait(false);

                Assert.Equal(new StringIdentifier("$actor$_cast_$direction$"), player.GetOnly<IDynamicAnimationBehavior>().BaseAnimationId);
            });
        }

        [Fact]
        private async Task UseSkill_HealSelfSingleUpdate10Seconds_ExpectedStatValue()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var player = _testAmenities.CreatePlayerInstance();
                var skillsBehavior = player.GetOnly<IHasSkillsBehavior>();
                var statsBehavior = player.GetOnly<IHasStatsBehavior>();

                statsBehavior.MutateStats(stats =>
                {
                    stats[new IntIdentifier(2)] = 1; // life current
                });

                var skill = _skillAmenity.EnsureHasSkill(
                    player,
                    new StringIdentifier("heal"));

                _mapGameObjectManager.MarkForAddition(player);
                await _mapGameObjectManager
                    .SynchronizeAsync()
                    .ConfigureAwait(false);

                await _testAmenities.ExecuteBetweenGameEngineUpdatesAsync(
                    TimeSpan.FromSeconds(10),
                    async () => await _skillHandlerFacade
                        .HandleSkillAsync(
                            player,
                            skill)
                        .ConfigureAwait(false));

                var statValue = await _statCalculationServiceAmenity
                    .GetStatValueAsync(
                        player,
                        new IntIdentifier(2)) // life current
                    .ConfigureAwait(false);

                Assert.Equal(6, statValue); // healed 10% max life over 5 turns (so... 1+5=6)
            });
        }

        [Fact]
        private async Task UseSkill_HealSelf10Updates1Second_ExpectedStatValue()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var player = _testAmenities.CreatePlayerInstance();
                var skillsBehavior = player.GetOnly<IHasSkillsBehavior>();
                var statsBehavior = player.GetOnly<IHasStatsBehavior>();

                statsBehavior.MutateStats(stats =>
                {
                    stats[new IntIdentifier(2)] = 1; // life current
                });

                var skill = _skillAmenity.EnsureHasSkill(
                    player,
                    new StringIdentifier("heal"));

                _mapGameObjectManager.MarkForAddition(player);
                await _mapGameObjectManager
                    .SynchronizeAsync()
                    .ConfigureAwait(false);

                var startTime = DateTime.UtcNow;
                _realTimeManager.SetTimeUtc(startTime);
                await _gameEngine
                    .UpdateAsync()
                    .ConfigureAwait(false);

                await _skillHandlerFacade
                    .HandleSkillAsync(
                        player,
                        skill)
                    .ConfigureAwait(false);

                for (int i = 0; i < 10; i++)
                {
                    _realTimeManager.SetTimeUtc(startTime + TimeSpan.FromMilliseconds(1000 * (i + 1)));
                    await _gameEngine
                        .UpdateAsync()
                        .ConfigureAwait(false);
                }

                var statValue = await _statCalculationServiceAmenity
                    .GetStatValueAsync(
                        player,
                        new IntIdentifier(2)) // life current
                    .ConfigureAwait(false);

                Assert.Equal(6, statValue); // healed 10% max life over 5 turns (so... 1+5=6)
            });
        }
    }
}
