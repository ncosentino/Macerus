using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummoningContext : ISummoningContext
    {
        private Lazy<ISummonEnchantmentBehavior> _lazySummonEnchantmentBehavior;
        private Lazy<IHasStatsBehavior> _lazySummonerStatsBehavior;

        public SummoningContext(
            IGameObject summoner,
            IGameObject summoningEnchantment,
            IEnumerable<IGameObject> summons)
        {
            Summoner = summoner;
            SummoningEnchantment = summoningEnchantment;
            Summons = summons.ToArray();

            _lazySummonEnchantmentBehavior = new Lazy<ISummonEnchantmentBehavior>(() =>
                SummoningEnchantment.GetOnly<ISummonEnchantmentBehavior>());
            _lazySummonerStatsBehavior = new Lazy<IHasStatsBehavior>(() =>
                Summoner.GetOnly<IHasStatsBehavior>());
        }

        public IGameObject Summoner { get; }

        public IGameObject SummoningEnchantment { get; }

        public IReadOnlyCollection<IGameObject> Summons { get; }

        public ISummonEnchantmentBehavior SummonEnchantmentBehavior => _lazySummonEnchantmentBehavior.Value;

        public IHasStatsBehavior SummonerStatsBehavior => _lazySummonerStatsBehavior.Value;
    }
}
