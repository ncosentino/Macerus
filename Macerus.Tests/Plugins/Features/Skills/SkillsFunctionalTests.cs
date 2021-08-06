using System;
using System.Linq;
using System.Threading.Tasks;

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

                var skillsBehavior = player.GetOnly<IHasSkillsBehavior>();

                // add skill if the actor doesn't have it already
                if (!skillsBehavior.Skills.Any(x => Equals(
                    x.GetOnly<IReadOnlyIdentifierBehavior>().Id,
                    new StringIdentifier("passive-green-glow"))))
                {
                    skillsBehavior.Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("passive-green-glow")) });
                }                    
                
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
        private async Task UseSkill_HealSelf_ExpectedStatValue()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var player = _testAmenities.CreatePlayerInstance();
                var skillsBehavior = player.GetOnly<IHasSkillsBehavior>();
                var statsBehavior = player.GetOnly<IHasMutableStatsBehavior>();

                statsBehavior.MutateStats(stats =>
                {
                    stats[new IntIdentifier(2)] = 1; // life current
                });

                // add skill if the actor doesn't have it already
                if (!skillsBehavior.Skills.Any(x => Equals(
                    x.GetOnly<IReadOnlyIdentifierBehavior>().Id,
                    new StringIdentifier("heal"))))
                {
                    skillsBehavior.Add(new[] { _skillAmenity.GetSkillById(new StringIdentifier("heal")) });
                }

                var skill = skillsBehavior.Skills.Single(x => Equals(
                    x.GetOnly<IReadOnlyIdentifierBehavior>().Id,
                    new StringIdentifier("heal")));

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

                Assert.Equal(10, statValue); // fully healed, but this might break easily
            });
        }
    }
}
