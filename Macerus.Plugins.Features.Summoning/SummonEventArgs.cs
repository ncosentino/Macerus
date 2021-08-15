using System;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Summoning
{
    public sealed class SummonEventArgs : EventArgs
    {
        public SummonEventArgs(IFrozenCollection<IGameObject> summons)
        {
            Summons = summons;
        }

        public IFrozenCollection<IGameObject> Summons { get; }
    }
}