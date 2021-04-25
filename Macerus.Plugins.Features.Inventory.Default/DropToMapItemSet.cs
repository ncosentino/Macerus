using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Containers.Api.LootDrops;
using Macerus.Plugins.Features.Inventory.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class DropToMapItemSet : IItemSet
    {
        private readonly ILootDropFactory _lootDropFactory;
        private readonly IMapGameObjectManager _mapGameObjectManager;

        public DropToMapItemSet(
            ILootDropFactory lootDropFactory,
            IMapGameObjectManager mapGameObjectManager)
        {
            _lootDropFactory = lootDropFactory;
            _mapGameObjectManager = mapGameObjectManager;
        }

        public event EventHandler<EventArgs> ItemsChanged;

        public IEnumerable<KeyValuePair<IIdentifier, IGameObject>> Items => Enumerable.Empty<KeyValuePair<IIdentifier, IGameObject>>();

        public IGameObject GetItem(IIdentifier id) => null;

        public bool CanSwapItems(
            IIdentifier itemIdToSwapOut,
            IGameObject itemToSwapIn)
        {
            Contract.Requires(
                itemIdToSwapOut == null,
                $"Expecting that '{nameof(itemIdToSwapOut)}' is always null.");
            Contract.RequiresNotNull(
                itemToSwapIn,
                $"Expecting that '{nameof(itemToSwapIn)}' is never null.");

            return true;
        }

        public SwapResult SwapItems(
            IIdentifier itemIdToSwapOut,
            IGameObject itemToSwapIn)
        {
            var playerWorldLocationBehavior = _mapGameObjectManager
                .GameObjects
                .First(x => x.Has<IPlayerControlledBehavior>())
                .GetOnly<IReadOnlyWorldLocationBehavior>();
            var loot = _lootDropFactory.CreateLoot(
                playerWorldLocationBehavior.X,
                playerWorldLocationBehavior.Y,
                false,
                new[]
                {
                    itemToSwapIn
                });
            _mapGameObjectManager.MarkForAddition(loot);
            return SwapResult.SuccessAndContinue;
        }
    }
}
