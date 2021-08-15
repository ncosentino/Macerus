using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.Summoning
{
    public interface ISummoningContext
    {
        ISummonEnchantmentBehavior SummonEnchantmentBehavior { get; }

        IGameObject Summoner { get; }

        IHasStatsBehavior SummonerStatsBehavior { get; }

        IGameObject SummoningEnchantment { get; }

        IReadOnlyCollection<IGameObject> Summons { get; }
    }
}
