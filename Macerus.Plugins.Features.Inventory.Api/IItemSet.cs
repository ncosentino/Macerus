using System;
using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IItemSet
    {
        event EventHandler<EventArgs> ItemsChanged;

        IEnumerable<KeyValuePair<IIdentifier, IGameObject>> Items { get; }

        IGameObject GetItem(IIdentifier id);

        bool CanSwapItems(IIdentifier itemIdToSwapOut, IGameObject itemToSwapIn);

        void SwapItems(IIdentifier itemIdToSwapOut, IGameObject itemToSwapIn);
    }
}
