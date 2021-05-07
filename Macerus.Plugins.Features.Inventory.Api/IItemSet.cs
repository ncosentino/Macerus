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

        SwapResult SwapItems(IIdentifier itemIdToSwapOut, IGameObject itemToSwapIn);
    }

    public static class IItemSetExtensions
    {
        public static bool CanAddItem(
            this IItemSet itemSet,
            IGameObject itemToAdd)
        {
            var result = itemSet.CanSwapItems(null, itemToAdd);
            return result;
        }

        public static SwapResult AddItem(
            this IItemSet itemSet,
            IGameObject itemToAdd)
        {
            var result = itemSet.SwapItems(null, itemToAdd);
            return result;
        }
    }
}
