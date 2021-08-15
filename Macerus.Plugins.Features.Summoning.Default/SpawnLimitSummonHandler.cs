using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SpawnLimitSummonHandler : IDiscoverableSummonHandler
    {
        private readonly Lazy<IStatCalculationServiceAmenity> _lazyStatCalculationServiceAmenity;
        private readonly ISummonLimitStatPairRepositoryFacade _summonLimitStatPairRepository;

        public SpawnLimitSummonHandler(
            Lazy<IStatCalculationServiceAmenity> lazyStatCalculationServiceAmenity,
            ISummonLimitStatPairRepositoryFacade summonLimitStatPairRepository)
        {
            _lazyStatCalculationServiceAmenity = lazyStatCalculationServiceAmenity;
            _summonLimitStatPairRepository = summonLimitStatPairRepository;
        }

        public async Task<ISummoningContext> HandleSummoningAsync(ISummoningContext summoningContext)
        {
            var summonDefinition = summoningContext
                .SummonEnchantmentBehavior
                .SummonDefinition;
            var summonLimitStatBehavior = summonDefinition.GetOnly<ISummonLimitStatBehavior>();
            var summonLimitStatPair = await _summonLimitStatPairRepository
                .GetPairByIdAsync(summonLimitStatBehavior.SummonLimitStatPairId)
                .ConfigureAwait(false);
            var summonLimit = await _lazyStatCalculationServiceAmenity
                .Value
                .GetStatValueAsync(
                    summoningContext.Summoner,
                    summonLimitStatPair.MaximumStatDefinitionId)
                .ConfigureAwait(false);

            var limitedSummons = new List<IGameObject>();
            foreach (var summonedActor in summoningContext.Summons)
            {
                if (summoningContext.SummonerStatsBehavior.BaseStats[summonLimitStatPair.CurrentStatDefinitionId] >=
                    summonLimit)
                {
                    break;
                }

                await summoningContext
                    .SummonerStatsBehavior
                    .MutateStatsAsync(async stats => stats[summonLimitStatPair.CurrentStatDefinitionId]++)
                    .ConfigureAwait(false);
                limitedSummons.Add(summonedActor);
            }

            var newContext = new SummoningContext(
                summoningContext.Summoner,
                summoningContext.SummoningEnchantment,
                limitedSummons);
            return newContext;
        }
    }
}
