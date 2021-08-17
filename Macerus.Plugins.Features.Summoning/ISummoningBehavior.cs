using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using NexusLabs.Collections.Generic;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Summoning
{
    public interface ISummoningBehavior : IBehavior
    {
        event EventHandler<SummonEventArgs> SummonedAsync;

        event EventHandler<SummonEventArgs> UnsummonedAsync;

        IReadOnlyDictionary<IGameObject, IFrozenCollection<IGameObject>> SummonsByEnchantment { get; }

        Task<int> UnsummonByEnchantmentAsync(IGameObject enchantment, int count);
    }
}
