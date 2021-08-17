using System;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Summoning
{
    public sealed class SummonEventArgs : EventArgs
    {
        public SummonEventArgs(
            IGameObject summoningEnchantment,
            IFrozenCollection<IGameObject> summons)
        {
            SummoningEnchantment = summoningEnchantment;
            Summons = summons;
        }

        public IGameObject SummoningEnchantment { get; }

        public IFrozenCollection<IGameObject> Summons { get; }
    }
}