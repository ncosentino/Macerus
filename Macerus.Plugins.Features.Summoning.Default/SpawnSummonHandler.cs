using System;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Spawning;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SpawnSummonHandler : IDiscoverableSummonHandler
    {
        private readonly Lazy<IActorSpawnerAmenity> _lazyActorSpawnerAmenity;

        public SpawnSummonHandler(Lazy<IActorSpawnerAmenity> lazyActorSpawnerAmenity)
        {
            _lazyActorSpawnerAmenity = lazyActorSpawnerAmenity;
        }

        public async Task<ISummoningContext> HandleSummoningAsync(ISummoningContext summoningContext)
        {
            var summonDefinition = summoningContext
                .SummonEnchantmentBehavior
                .SummonDefinition;
            var summonedActors = _lazyActorSpawnerAmenity
                .Value
                .SpawnActorsFromSpawnTableId(summonDefinition
                    .GetOnly<SummonSpawnTableBehavior>()
                    .SpawnTableId);
            var newContext = new SummoningContext(
                summoningContext.Summoner,
                summoningContext.SummoningEnchantment,
                summonedActors);
            return newContext;
        }
    }
}
